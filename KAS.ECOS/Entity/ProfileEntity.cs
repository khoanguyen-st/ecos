using KAS.ECOS.API.Code;
using System.Text.Json.Serialization;

namespace KAS.ECOS.API.Entity
{

    public class ProfileEntity
    {
        public ProfileEntity() { }
        private string _password = "";


        public string IP { get; set; } = "";
        public eProfileDatabase typeProfile { get; set; }
        public string username { get; set; } = "";
        public string password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = Core.MaHoa.strRC2Encryption(value);
            }

        }
        [JsonIgnore]

        public string passwordDecrypt
        {
            get
            {
                return Core.MaHoa.strRC2Decryption(this.password);
            }
        }
        public int Port { get; set; } = 80;
        /// <summary>
        /// Database Name hoặc API Name
        /// </summary>
        public string Name { get; set; } = "";


    }
    public enum eProfileDatabase : byte
    {

        ServerAPI = 1,
        MongoDB = 11,
        PostgreSQL = 12
    }
}
