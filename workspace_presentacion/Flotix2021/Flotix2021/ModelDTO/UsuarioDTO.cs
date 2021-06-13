
namespace Flotix2021.ModelDTO
{
    public class UsuarioDTO
    {
        private string _id;

        public string id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _nombre;

        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private string _email;
        
        public string email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _idRol;

        public string idRol
        {
            get { return _idRol; }
            set { _idRol = value; }
        }
        
        private RolDTO _rol;

        public RolDTO rol
        {
            get { return _rol; }
            set { _rol = value; }
        }
        
        private string _pwd;

        public string pwd
        {
            get { return _pwd; }
            set { _pwd = value; }
        }
    }
}
