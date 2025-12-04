using Microsoft.EntityFrameworkCore.Migrations.Operations;

public interface DAOFactory
{
    DAOUser CreateUser();

    DAOUser buscarUserMail();

    DAOUser buscarUserId();

    DAOUser update();

    DAOUser Delete();

    DAOUser GetUsers();

    DAOUser CreateRelationFollow();

    DAOUser buscarRelacion();

    DAOUser DeleteRelationFollow();

    DAOUser filtrarUser();

    DAOPost createPost();

    DAOReaction createReaction();

    DAOComment CreateComment();

    DAOBan CreateBan();




    
}

