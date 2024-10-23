namespace Gp.Service.Extensions
{
    public class FilterBody
    {
        public FilterConditionType Condition { get; set; }

        public string Property { get; set; }

        public string SubProperty { get; set; }

        public object ConditionStart { get; set; }

        public object ConditionEnd { get; set; }

        public bool IsEmpty()
        {
            if (Condition == FilterConditionType.Igual || Condition == FilterConditionType.Contenha || Condition == FilterConditionType.NaoContenha || Condition == FilterConditionType.Igual || Condition == FilterConditionType.MaiorOuIgual || Condition == FilterConditionType.Maior || Condition == FilterConditionType.MenorOuIgual || Condition == FilterConditionType.Menor || Condition == FilterConditionType.ComeceCom || Condition == FilterConditionType.TermineCom || Condition == FilterConditionType.Diferente)
            {
                if (ConditionStart != null)
                {
                    return string.IsNullOrEmpty(ConditionStart.ToString());
                }

                return true;
            }

            if (ConditionStart == null || string.IsNullOrEmpty(ConditionStart.ToString()))
            {
                if (ConditionEnd != null)
                {
                    return string.IsNullOrEmpty(ConditionEnd.ToString());
                }

                return true;
            }

            return false;
        }
    }
}
