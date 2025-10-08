local L0_0, L1_1
L0_0 = 1
HOSTILE = L0_0
L0_0 = 0
INACTIVE = L0_0
L0_0 = 850
LEASH_RADIUS = L0_0
L0_0 = 750
LEASH_PROTECTION_RADIUS = L0_0
L0_0 = 500
FEAR_WANDER_DISTANCE = L0_0
L0_0 = 0.1
REGEN_PERCENT_PER_SECOND = L0_0
function L0_0(A0_2)
  SetState(AI_ATTACK)
  SetMyLeashedPos()
  InitTimer("TimerRetreat", 0.5, true)
  InitTimer("TimerAttack", 0, true)
  InitTimer("TimerFeared", 0.5, true)
  InitTimer("TimerRegen", 1, true)
  InitTimer("TimerTaunt", 0.5, true)
  StopTimer("TimerFeared")
  StopTimer("TimerRegen")
  StopTimer("TimerTaunt")
end
OnInit = L0_0
function L0_0()
  SetStateAndMoveToLeashedPos(AI_RETREAT)
  ClearValidTargets()
end
Retreat = L0_0

-- Helper to aggro the whole camp
function AggroWholeCamp(target)
  local campMinions = GetCampMinions and GetCampMinions() or {GetSelf()}
  for _, minion in ipairs(campMinions) do
    if minion ~= nil and not minion:IsDead() then
      minion:StopTimer("TimerRegen")
      minion:SetStateAndCloseToTarget(AI_ATTACK, target)
      minion:SetRoamState(HOSTILE)
      minion._lastKnownTargetPosition = target:GetPosition()
      minion._isSearching = false
      minion._searchTimer = 0
      minion._isReturning = false
    end
  end
end

-- Helper to retreat the whole camp
function RetreatWholeCamp()
  local campMinions = GetCampMinions and GetCampMinions() or {GetSelf()}
  for _, minion in ipairs(campMinions) do
    if minion ~= nil and not minion:IsDead() then
      minion:SetStateAndMoveToLeashedPos(AI_RETREAT)
      minion:ClearValidTargets()
      minion._isSearching = false
      minion._searchTimer = 0
      minion._isReturning = true
      minion._lastKnownTargetPosition = minion:GetMyLeashedPos()
      minion:SetTarget(nil)
    end
  end
end

function IsCampLeader()
  local campMinions = GetCampMinions and GetCampMinions() or {GetSelf()}
  return campMinions[1] == GetSelf()
end

function GetCampLeader()
  local campMinions = GetCampMinions and GetCampMinions() or {GetSelf()}
  for _, minion in ipairs(campMinions) do
    if minion ~= nil and not minion:IsDead() then
      return minion
    end
  end
  return GetSelf()
end

function IsCampLeader()
  return GetCampLeader() == GetSelf()
end

function AggroWholeCamp(target)
  local campMinions = GetCampMinions and GetCampMinions() or {GetSelf()}
  for _, minion in ipairs(campMinions) do
    if minion ~= nil and not minion:IsDead() then
      minion:StopTimer("TimerRegen")
      minion:SetStateAndCloseToTarget(AI_ATTACK, target)
      minion:SetRoamState(HOSTILE)
    end
  end
end

function RetreatWholeCamp()
  local campMinions = GetCampMinions and GetCampMinions() or {GetSelf()}
  for _, minion in ipairs(campMinions) do
    if minion ~= nil and not minion:IsDead() then
      minion:SetStateAndMoveToLeashedPos(AI_RETREAT)
      minion:ClearValidTargets()
    end
  end
end

function L0_0()
  local L0_3, L1_4, L2_5, L3_6, L4_7
  L0_3 = GetState
  L0_3 = L0_3()
  L1_4 = AI_HALTED
  if L0_3 == L1_4 then
    return
  end
  L1_4 = GetMaxHP
  L1_4 = L1_4()
  L2_5 = REGEN_PERCENT_PER_SECOND
  L2_5 = L1_4 * L2_5
  L3_6 = GetHP
  L3_6 = L3_6()
  if L3_6 > 0 then
    L4_7 = L3_6 + L2_5
    SetHP(L4_7)
  end
end
TimerRegen = L0_0
function L0_0(A0_8, A1_9)
  if GetState() == AI_HALTED then
    return
  end
  if A0_8 == ORDER_ATTACKTO then
    StopTimer("TimerRegen")
    SetStateAndCloseToTarget(AI_ATTACK, A1_9)
    SetRoamState(HOSTILE)
    return true
  end
  Error("Unsupported Order")
  return false
end
OnOrder = L0_0
function L0_0(A0_10)
  if not IsCampLeader() then return end
  if GetState() == AI_HALTED then
    return
  end
  AddValidTarget(A0_10)
  AggroWholeCamp(A0_10)
end
OnTakeDamage = L0_0
function L0_0(A0_11, A1_12)
  if not IsCampLeader() then return end
  if GetState() == AI_HALTED then
    return
  end
  AddValidTarget(A0_11)
  AggroWholeCamp(A0_11)
end
LeashedCallForHelp = L0_0
function L0_0(A0_16, A1_17)
  local L2_18, L3_19
  L2_18 = GetState
  L2_18 = L2_18()
  L3_19 = AI_HALTED
  if L2_18 == L3_19 then
    return
  end
  L3_19 = GetOwner
  L3_19 = L3_19(A1_17)
  if L3_19 == nil then
    L3_19 = GetGoldRedirectTarget(A1_17)
  end
  if L3_19 ~= nil then
    SetStateAndCloseToTarget(AI_ATTACK, L3_19)
  else
    FindNewTarget()
  end
end
OnTargetLost = L0_0
function L0_0()
  if GetState() == AI_HALTED then
    return
  end
  tauntTarget = GetTauntTarget()
  if tauntTarget ~= nil and GetState() ~= AI_FEARED then
    StopTimer("TimerRegen")
    SetStateAndCloseToTarget(AI_TAUNTED, tauntTarget)
    SetRoamState(HOSTILE)
  end
end
OnTauntBegin = L0_0
function L0_0()
  if GetState() == AI_HALTED then
    return
  end
  StopTimer("TimerTaunt")
  tauntTarget = GetTauntTarget()
  if tauntTarget ~= nil and GetState() ~= AI_FEARED then
    StopTimer("TimerRegen")
    SetStateAndCloseToTarget(AI_ATTACK, tauntTarget)
    SetRoamState(HOSTILE)
  else
    NetSetState(AI_ATTACK)
    TimerRetreat()
    TimerAttack()
  end
end
OnTauntEnd = L0_0
function L0_0()
  if GetState() == AI_HALTED then
    return
  end
  tauntTarget = GetTauntTarget()
  if tauntTarget ~= nil and GetState() ~= AI_FEARED then
    StopTimer("TimerRegen")
    SetStateAndCloseToTarget(AI_TAUNTED, tauntTarget)
    SetRoamState(HOSTILE)
  end
end
TimerTaunt = L0_0
function L0_0()
  if GetState() == AI_HALTED then
    return
  end
  wanderPoint = MakeWanderPoint(GetFearLeashPoint(), FEAR_WANDER_DISTANCE)
  SetStateAndMove(AI_FEARED, wanderPoint)
  SetRoamState(INACTIVE)
  TurnOffAutoAttack(STOPREASON_IMMEDIATELY)
  ResetAndStartTimer("TimerFeared")
end
OnFearBegin = L0_0
function L0_0()
  if GetState() == AI_HALTED then
    return
  end
  StopTimer("TimerFeared")
  SetRoamState(HOSTILE)
  NetSetState(AI_ATTACK)
  TimerRetreat()
  TimerAttack()
end
OnFearEnd = L0_0
function L0_0()
  if GetState() == AI_HALTED then
    return
  end
  wanderPoint = MakeWanderPoint(GetFearLeashPoint(), FEAR_WANDER_DISTANCE)
  SetRoamState(INACTIVE)
  SetStateAndMove(AI_FEARED, wanderPoint)
end
TimerFeared = L0_0
function L0_0()
  local L0_20, L1_21, L2_22, L3_23, L4_24
  L0_20 = GetRoamState
  L0_20 = L0_20()
  L1_21 = INACTIVE
  if L0_20 == L1_21 then
    return
  end
  L0_20 = GetState
  L0_20 = L0_20()
  L1_21 = AI_HALTED
  if L0_20 == L1_21 then
    return
  end
  L1_21 = GetDistToLeashedPos
  L1_21 = L1_21()
  L2_22 = GetTarget
  L2_22 = L2_22()
  L3_23 = GetMyLeashedPos
  L3_23 = L3_23()
  L4_24 = LEASH_RADIUS
  L4_24 = L4_24 + 1
  if L2_22 ~= nil then
    L4_24 = DistanceBetweenObjectAndPoint(L2_22, L3_23)
  end
  if L1_21 > LEASH_PROTECTION_RADIUS and L1_21 < LEASH_RADIUS and L4_24 > LEASH_RADIUS and L0_20 ~= AI_RETREAT and GetLeashCounter() < 2 then
    FindNewTarget()
    L2_22 = GetTarget()
    if L2_22 ~= nil then
      SetLeashCounter(GetLeashCounter() + 1)
    end
  elseif L1_21 > LEASH_RADIUS and L0_20 ~= AI_RETREAT then
    isLeashing = true
    ResetAndStartTimer("TimerRegen")
    RetreatWholeCamp()
  end
  if L0_20 == AI_RETREAT and IsMovementStopped() == true then
    if GetDistToRetreat() < 100 then
      OnStoppedMoving()
    else
      RetreatWholeCamp()
    end
  end
end
TimerRetreat = L0_0
function L0_0()
  if GetState() == AI_HALTED then
    return
  end
  if GetState() == AI_RETREAT then
    SetLeashCounter(0)
    SetState(AI_ATTACK)
    SetRoamState(GetOriginalState())
  end
end
OnStoppedMoving = L0_0
function L0_0()
  local L0_25, L1_26
  L0_25 = GetState
  L0_25 = L0_25()
  L1_26 = AI_HALTED
  if L0_25 == L1_26 then
    return
  end
  L1_26 = GetRoamState
  L1_26 = L1_26()
  if L1_26 ~= INACTIVE then
    L1_26 = AI_RETREAT
  elseif L0_25 == L1_26 then
    return
  end
  L1_26 = AI_ATTACK
  if L0_25 ~= L1_26 then
    L1_26 = AI_TAUNTED
  elseif L0_25 == L1_26 then
    L1_26 = GetTarget
    L1_26 = L1_26()
    if L1_26 ~= nil then
      if TargetInAttackRange() then
        TurnOnAutoAttack(L1_26)
      elseif TargetInCancelAttackRange() == false then
        TurnOffAutoAttack(STOPREASON_MOVING)
      end
    else
      FindNewTarget()
    end
  end
end
TimerAttack = L0_0
function L0_0()
  local L0_27, L1_28, L2_29, L3_30
  L0_27 = GetState
  L0_27 = L0_27()
  L1_28 = AI_HALTED
  if L0_27 == L1_28 then
    return
  end
  L1_28 = GetRoamState
  L1_28 = L1_28()
  L2_29 = INACTIVE
  if L1_28 ~= L2_29 then
    L1_28 = GetState
    L1_28 = L1_28()
    L2_29 = AI_RETREAT
  elseif L1_28 == L2_29 then
    return
  end
  L1_28 = GetMyLeashedPos
  L1_28 = L1_28()
  L2_29 = FindValidTargetNearPosition
  L3_30 = L1_28
  L2_29 = L2_29(L3_30, LEASH_RADIUS)
  L3_30 = LEASH_RADIUS
  L3_30 = L3_30 + 1
  if L2_29 ~= nil then
    L3_30 = DistanceBetweenObjectAndPoint(L2_29, L1_28)
  end
  if L2_29 ~= nil and L3_30 <= LEASH_RADIUS then
    StopTimer("TimerRegen")
    SetStateAndCloseToTarget(AI_ATTACK, L2_29)
  else
    ResetAndStartTimer("TimerRegen")
    Retreat()
  end
end
FindNewTarget = L0_0
function L0_0()
  StopTimer("TimerRetreat")
  StopTimer("TimerAttack")
  StopTimer("TimerFeared")
  StopTimer("TimerRegen")
  TurnOffAutoAttack(STOPREASON_IMMEDIATELY)
  NetSetState(AI_HALTED)
end
HaltAI = L0_0
