using System.Linq;

namespace Game.Data
{
    public class UserManager
    {
        private Model model = new Model();

        public User FindUserById(int id)
        {
            return model.Users.Where(u => u.Id == id).SingleOrDefault();
        }
    }
}
