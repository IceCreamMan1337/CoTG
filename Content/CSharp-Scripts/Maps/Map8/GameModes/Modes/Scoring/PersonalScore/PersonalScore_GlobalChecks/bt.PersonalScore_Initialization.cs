namespace BehaviourTrees;


class PersonalScore_InitializationClass : OdinLayout
{


    public bool PersonalScore_Initialization(

                out bool IsFirstBlood)
    {

        bool _IsFirstBlood = default;
        bool result =
                          // Sequence name :Sequence

                          SetVarBool(
                                out _IsFirstBlood,
                                true)

                    ;
        IsFirstBlood = _IsFirstBlood;
        return result;
    }
}

