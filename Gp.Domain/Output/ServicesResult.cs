namespace Gp.Domain.Output
{
    public class ServicesResult
    {
        public object Data { get; set; }
        public Dictionary<string, string> Error { get; set; } = new Dictionary<string, string>();


        public ServicesResult()
        {

        }

        public ServicesResult(Dictionary<string, string> error)
        {
            Error = error;
        }

        public ServicesResult(object result)
        {
            Data = result;
        }

        public ServicesResult(string campo, string message)
        {
            AdicionarErro(campo, message);
        }

        public ServicesResult(object data, Dictionary<string, string> error)
        {
            Data = data;
            Error = error;
        }

        public void AdicionarErro(string campo, string texto)
        {
            Error ??= new Dictionary<string, string>();
            Error.Add(campo, texto);
        }
    }
}
