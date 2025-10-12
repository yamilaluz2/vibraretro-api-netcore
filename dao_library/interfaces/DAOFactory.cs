public interface DAOFactory
{
    DAOUser CreateUser();

    DAOUser buscarUserMail();

    DAOUser buscarUserId();

    DAOUser update();

    DAOUser Delete();
    
    DAOUser GetUsers();




    
}

