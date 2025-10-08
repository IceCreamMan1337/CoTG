using System;
using System.Linq;
using System.Collections.Generic;
using CoTGEnumNetwork;
using Newtonsoft.Json;
using JO = Newtonsoft.Json.JsonObjectAttribute;
using MS = Newtonsoft.Json.MemberSerialization;

namespace CoTG.CoTGServer.GameObjects.StatsNS
{
    [JO(MS.OptIn)]
    public class MoveSpeed
    {
        [JsonProperty] private float _flatPerm;
        [JsonProperty] private float _flatBonusPerm;
        [JsonProperty] private float _percentBonusPerm;
        [JsonProperty] private List<float> _multiplicativeBonusesPerm = new();
        [JsonProperty] private float _slowResistPerm;

        [JsonProperty] private float _flatTemp;
        [JsonProperty] private float _flatBonusTemp;
        [JsonProperty] private float _percentBonusTemp;
        [JsonProperty] private List<float> _multiplicativeBonusesTemp = new();
        [JsonProperty] private float _slowResistTemp;

        private float _flat => _flatPerm + _flatTemp;
        private float _flatBonus => _flatBonusPerm + _flatBonusTemp;
        private float _percentBonus => _percentBonusPerm + _percentBonusTemp;
        private IEnumerable<float> _multiplicativeBonuses => _multiplicativeBonusesPerm.Concat(_multiplicativeBonusesTemp);
        private float _slowResist => _slowResistPerm + _slowResistTemp;

        public float Flat
        {
            get => _flat;
            set => _flatPerm = value * (_dirty = 1);
        }

        public float FlatBonus
        {
            get => _flatBonus;
            set => _flatBonusPerm = value * (_dirty = 1);
        }

        public float PercentBonus
        {
            get => _percentBonus;
            set => _percentBonusPerm = value * (_dirty = 1);
        }

        public float SlowResist
        {
            get => _slowResist;
            set => _slowResistPerm = value * (_dirty = 1);
        }

        private bool StatModified =>
            Extensions.COMPARE_EPSILON < Math.Abs(_flat)
            || Extensions.COMPARE_EPSILON < Math.Abs(_flatBonus)
            || Extensions.COMPARE_EPSILON < Math.Abs(_percentBonus)
            || _multiplicativeBonuses.Count() <= 0
            || Extensions.COMPARE_EPSILON < Math.Abs(_slowResist);

        private float _total;
        private int _dirty = 0;
        public float Total => (_dirty-- != 0) ? _total = CalculateTrueMoveSpeed() : _total;

        public void IncBaseFlat(float by)
        {
            _flatTemp += by;
            if (by != 0) _dirty = 1;
        }
        public void IncFlatBonus(float by)
        {
            _flatBonusTemp += by;
            if (by != 0) _dirty = 1;
        }
        public void IncFlatBonusPerm(float by)
        {
            _flatBonusPerm += by;
            if (by != 0) _dirty = 1;
        }
        public void IncBonusPercent(float by)
        {
            _percentBonusTemp = Math.Clamp(_percentBonusTemp + by, -1f, 1f);
            if (by != 0) _dirty = 1;
        }
        public void IncBonusPercentPerm(float by)
        {
            _percentBonusPerm += by;
            if (by != 0) _dirty = 1;
        }
        public void IncBonusMultiplicativePercent(float by)
        {
            if (by != 0)
            {
                _multiplicativeBonusesTemp.Add(by);
                _dirty = 1;
            }
        }
        public void IncBonusMultiplicativePercentPerm(float by)
        {
            if (by != 0)
            {
                _multiplicativeBonusesPerm.Add(by);
                _dirty = 1;
            }
        }

        public void ApplyStatModifier(MoveSpeed msMod)
        {
            if (msMod.StatModified)
            {
                _flatPerm += msMod._flat;
                _flatBonusPerm += msMod._flatBonus;
                foreach (var bonus in msMod._multiplicativeBonuses)
                    _multiplicativeBonusesPerm.Add(bonus);
                _slowResistPerm += msMod._slowResist;
                _dirty = 1;
            }
        }

        public void RemoveStatModifier(MoveSpeed msMod)
        {
            if (msMod.StatModified)
            {
                _flatPerm -= msMod._flat;
                _flatBonusPerm -= msMod._flatBonus;
                foreach (var bonus in msMod._multiplicativeBonuses)
                    _multiplicativeBonusesPerm.Remove(bonus);
                _slowResistPerm -= msMod._slowResist;
                _dirty = 1;
            }
        }

        private float CalculateTrueMoveSpeed()
        {
            float speed = (_flat + _flatBonus) * (1 + _percentBonus);

            if (_multiplicativeBonuses.Count() > 0)
            {
                var bonuses = _multiplicativeBonuses.Where(p => p > 0);
                foreach (var bonus in bonuses)
                {
                    speed *= 1 + bonus;
                }

                var slows = _multiplicativeBonuses.Where(p => p < 0).OrderDescending();
                float nextSlowStrength = 1; // the strongest one fully applied
                foreach (var slow in slows)
                {
                    //TODO: Determine the order of application of nextSlowStrength and _slowResist
                    speed *= 1 + (slow * nextSlowStrength) - _slowResist;
                    nextSlowStrength = 0.35f; // the others applied with 65% reduced effectiveness
                }
            }

            if (speed > 490.0f)
            {
                speed = (speed * 0.5f) + 230; // 230 = (415*(1-0.5f) + (490-415)*(0.8f-0.5f)
            }
            else if (speed >= 415.0f)
            {
                speed = (speed * 0.8f) + 83; // 83 = 415*(1-0.8f)
            }
            else if (speed < 220.0f)
            {
                speed = (speed * 0.5f) + 110; // 110 = 220*(1-0.5f)
            }

            return speed;
        }

        public void ClearSlows()
        {
            _multiplicativeBonusesPerm.RemoveAll(p => p < 0);
            _dirty = 1;
        }

        internal void ResetToPermanent()
        {
            _flatTemp = 0;
            _flatBonusTemp = 0;
            _percentBonusTemp = 0;
            _multiplicativeBonusesTemp.Clear();
            _slowResistTemp = 0;
            _dirty = 1;
        }
    }
}