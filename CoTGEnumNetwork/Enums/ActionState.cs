using System;

namespace CoTGEnumNetwork.Enums
{
    [Flags]
    public enum ActionState : uint
    {
        CAN_ATTACK = 0x1, //1
        CAN_CAST = 0x2, //2
        CAN_MOVE = 0x4, //3
        //CAN_NOT_MOVE = 0x0, //this one is stupid 
        //


        STEALTHED = 0x8,      //CAN_NOT_MOVE //4
        REVEAL_SPECIFIC_UNIT = 0x10,   //STEALTHED //5
        TAUNTED = 0x20,  //6

        FEARED = 0x40, //7
        // FEARED = 1 << 7,
        //  IS_FLEEING = 1 << 8,
        CAN_NOT_ATTACK = 0x80,  // mSuppressed //8
        TARGETABLE = 0x100, //found where is targetable // IS_ASLEEP   // mSleep  //9 
        IS_NEAR_SIGHTED = 0x200, //10

        //  FEARED = 1 << 9, //verify
        /*
         * BBSetNearSight(
        */
        IS_GHOSTED = 0x400, //11
        IS_GHOSTEDPROOF = 0x800, //12
        CHARMED = 0x1000, //13
        NO_RENDER = 0x2000, //14
        FORCE_RENDER_PARTICLES = 0x4000, //15
        DODGEPIERCING = 0x8000, //16
        /*
        LuaBuildingBlockHelper::BBSetDodgePiercing(
        */
        DISABLEAMBIENTGOLD = 0x10000, //17
        /*
        LuaBuildingBlockHelper::BBSetStateDisableAmbientGold(
        */
        DISABLEAMBIENTXP = 0x20000, //18
        BRUSHVISIBILITYFAKE = 0x40000, //19 


        //  TARGETABLE = 1 << 30,



        REMAIN = 1 << 30 //REMAIN

        /*
 bool __thiscall obj_AI_Minion::ShouldRenderParticles(obj_AI_Minion *this)
{
CharacterState::CompressedStates v2; // eax
bool result; // al

result = 1;
if ( (this->mMinionFlags & 4) == 0 )
{
if ( !(unsigned __int8)NetVisibilityObject::IsVisible((NetVisibilityObject *)this, (int)&this->mNetVisibility, 1) )
return 0;
v2.0 = ($7F8493563DDD8E21704FF3CD4D507686)this->CharState.mStates.mValue;
if ( (*(_WORD *)&v2.0 & 0x2000) != 0 && (*(_WORD *)&v2.0 & 0x4000) == 0 )
return 0;
}
return result;
}
 */



        /*

        AI_MOVETO = 0x20
        AI_HOLD = 0x1000
        AI_STOP = 0x40 


        */


        /* 
         0 == false 
         1 == true  

            LuaSpellScriptHelper::SetCanAttack   
            LuaSpellScriptHelper::SetCanCast
        LuaSpellScriptHelper::SetCanMove
            
            CAN_ATTACK = 1 << 0 & 1 ,   LuaSpellScriptHelper::GetCanAttack              1
                                           CharacterState::SetCanAttack    
        CAN_CAST = 1 << 1,                                                               2

        CAN_MOVE = 1 << 2 & 1 , obj_AI_Base::CanMoveDebug  LuaSpellScriptHelper::GetCanMove 4
       

                                                                                    
         STEALTHED = 1 << 3 & 1 ,          obj_AI_Base::IsStealthed                    

             CAN_NOT_MOVE = 1 << 4,                GetCanMove                       
                                                               3                   8
                                                               4                   10 

        REVEAL_SPECIFIC_UNIT = 1 << 5,                               5              20 
         TAUNTED = 1 << 6,               IssueOrder taunted          6              40
    CAN_NOT_ATTACK = 1 << 7,     LuaSpellScriptHelper::GetSuppressed      7         80
                                                                                8    100  // mSuppressed   
                IS_ASLEEP = 1 << 9,                // mSleep                    

                IS_NEAR_SIGHTED = 1 << 9 & 1,  obj_AI_Base::IsNearSighted     9       200   


         IS_GHOSTED = 1 << 10,                 LuaSpellScriptHelper::GetGhosted  10    400
                IS_GHOSTEDPROOF = 1 << 11,     LuaSpellScriptHelper::GetGhostProof 11 800
                CHARMED = 1 << 13,             ar_AICharmedAcquisitionRange.var  12    1000
                NO_RENDER = 1 << 13,           obj_AI_Base::IsVisible           13    2000
                FORCE_RENDER_PARTICLES = 1 << 14,   GetForceRenderParticles   14      4000
                DODGEPIERCING = 1 << 16,                                      15      8000
         DISABLEAMBIENTGOLD = 1 << 17,                 SetDisableAmbientGold  16      10000 

          DISABLEAMBIENTXP = 1 << 18,                                         17       20 000 

                BRUSHVISIBILITYFAKE = 1 << 18,  SetBrushVisibilityFake         18      40000
                                                                               19      80000
                TARGETABLE = 1 << 32


         */




        /* 

        void __userpurge CharacterState::SetSilenced(CharacterState *this@<ecx>, unsigned int *a2@<esi>, bool newState)
{
CharacterState::SetCanCast(this, a2, !newState);
}

//----- (008B96F0) --------------------------------------------------------
void __userpurge CharacterState::SetRooted(CharacterState *this@<ecx>, unsigned int *a2@<eax>, bool newState)
{
CharacterState *v4; // ecx

CharacterState::SetCanMove(this, a2, !newState);
CharacterState::SetCanAttack(v4, a2, !newState);
}
// 8B9703: variable 'v4' is possibly undefined

//----- (008B9710) --------------------------------------------------------
void __userpurge CharacterState::SetDisarmed(CharacterState *this@<ecx>, unsigned int *a2@<esi>, bool newState)
{
CharacterState::SetCanAttack(this, a2, !newState);
}

//----- (008B9730) --------------------------------------------------------
void __userpurge CharacterState::SetNetted(CharacterState *this@<ecx>, unsigned int *a2@<esi>, bool newState)
{
CharacterState::SetCanMove(this, a2, !newState);
}

//----- (008B9750) --------------------------------------------------------
void __userpurge CharacterState::SetPacified(CharacterState *this@<ecx>, unsigned int *a2@<eax>, bool newState)
{
CharacterState *v4; // ecx

CharacterState::SetCanAttack(this, a2, !newState);
CharacterState::SetCanCast(v4, a2, !newState);
}
// 8B9763: variable 'v4' is possibly undefined

//----- (008B9770) --------------------------------------------------------
void __userpurge CharacterState::SetStunned(CharacterState *this@<ecx>, unsigned int *a2@<eax>, bool newState)
{
CharacterState *v4; // ecx
CharacterState *v5; // ecx

CharacterState::SetCanMove(this, a2, !newState);
CharacterState::SetCanAttack(v4, a2, !newState);
CharacterState::SetCanCast(v5, a2, !newState);
}
// 8B9783: variable 'v4' is possibly undefined
// 8B9789: variable 'v5' is possibly undefined

//----- (008B97A0) --------------------------------------------------------
void __userpurge CharacterState::SetSleep(CharacterState *this@<ecx>, int a2@<eax>, bool newState)
{
unsigned __int16 v4; // ax
int v5; // eax
CharacterState *v6; // ecx
CharacterState *v7; // ecx

LOBYTE(v4) = 0;
HIBYTE(v4) = newState;
v5 = *(_DWORD *)a2 ^ (*(_DWORD *)a2 ^ v4) & 0x100;
if ( *(_DWORD *)a2 != v5 )
{
this = *(CharacterState **)(a2 + 12);
*(_DWORD *)a2 = v5;
if ( this )
  (*(void (__thiscall **)(CharacterState *, _DWORD, _DWORD, int))(this->mStates.mValue.Raw + 12))(
    this,
    *(_DWORD *)(a2 + 8),
    *(_DWORD *)(a2 + 4),
    v5);
}
CharacterState::SetCanMove(this, (unsigned int *)a2, !newState);
CharacterState::SetCanAttack(v6, (unsigned int *)a2, !newState);
CharacterState::SetCanCast(v7, (unsigned int *)a2, !newState);
}
// 8B97D8: variable 'this' is possibly undefined
// 8B97DE: variable 'v6' is possibly undefined
// 8B97E4: variable 'v7' is possibly undefined



        */
    }
}