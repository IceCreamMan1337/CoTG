namespace Buffs
{
    public class SwainBeamTransition : BuffScript
    {
        int casterID;
        public override void OnActivate()
        {
            casterID = PushCharacterData("SwainNoBird", owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            PopCharacterData(owner, casterID);
        }
    }
}
namespace PreLoads
{
    public class SwainBeamTransition : IPreLoadScript
    {
        public void Preload()
        {
            PreloadCharacter("swainnobird");
        }
    }
}