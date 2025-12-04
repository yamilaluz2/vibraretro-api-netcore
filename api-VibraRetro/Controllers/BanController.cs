using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_VibraRetro.Controllers;

[ApiController]
[Route("[controller]")]
public class BanController : ControllerBase
{

    private DAOFactory df;
    private IToken tokenService;
    private readonly ILogger<BanController> _logger;

    public BanController(ILogger<BanController> logger, DAOFactory df, IToken token)
    {
        _logger = logger;
        this.df = df;
        this.tokenService = token;
    }

    [Authorize]
    [HttpPost("ban")]
    public IActionResult Ban ([FromBody] PostBanDTORequest request)
    {
        var userIdString = User.FindFirst("UserId")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return Unauthorized("Token inválido o sin UserId.");
        }

        int userId = int.Parse(userIdString);

        User admin = this.df.UserDAOFactory().ExisteId(userId);
        User user = this.df.UserDAOFactory().ExisteId(request.userId);
        if (admin != null && user != null)
        {
            Ban banUser = new Ban
            {
                User =user,
                Reason=request.reason,
                StartDate = request.startDate,
                EndDate = request.endDate,
                Admin = admin

            };
            user.State = true;
            this.df.UserDAOFactory().save(user);
            
            this.df.BanDAOFactory().CreateBanDAO(banUser);

            return Ok(new CommonDTOResponse
            {
                success=true,
                message= "usuario Baneado Correctamente."
            });
        }
        return BadRequest(new CommonDTOResponse
        {
            success=false,
            message="usuario o admin no encontrado."
        });
    }


}
