namespace PassengerPortal.Shared.Models
{
    public class Passenger : User
    {

        public Passenger()
        {
            Roles.Add(new Role { RoleName = "Passenger" });
        }
    }
}
