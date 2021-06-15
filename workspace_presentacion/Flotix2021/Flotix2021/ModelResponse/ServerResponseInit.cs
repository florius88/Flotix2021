namespace Flotix2021.ModelResponse
{
    class ServerResponseInit
    {
        private string _msg;

        public string msg
        {
            get { return _msg; }
            set { _msg = value; }
        }

        private ErrorBean _error;

        public ErrorBean error
        {
            get { return _error; }
            set { _error = value; }
        }
    }
}
