using Newtonsoft.Json;
using PremiereAppASP.Models;

namespace PremiereAppASP.Tools {
    public class SessionManager {

        private readonly ISession _session;

        public SessionManager(IHttpContextAccessor httpContextAccessor) {

            _session = httpContextAccessor.HttpContext.Session;
        }

        public User? ConnecterUser {

            get { 
                return string.IsNullOrEmpty(_session.GetString("connectedUser")) ?
                    null :
                    JsonConvert.DeserializeObject<User>( _session.GetString( "connectedUser" ) ); 
            }
            set { 
                _session.SetString("connectedUser", JsonConvert.SerializeObject(value));
            }
        }

        public void Logout() {

            _session.Clear();
        }
    }
}
