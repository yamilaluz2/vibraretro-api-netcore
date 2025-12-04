public class Ban
{
    private Int32 id; 
    public Int32 Id { get { return id; } set { id = value; } }

    private  User? user;
    public  virtual User? User { get { return user;} set { user = value;} }

    private string? reason;
    public string? Reason { get { return reason; } set { reason = value; } }

    private DateTime startDate;
    public DateTime StartDate { get { return startDate; } set { startDate = value; } }

    private DateTime? endDate;
    public DateTime? EndDate { get { return endDate; } set { endDate = value; } }

    private User? admin;
    public virtual User? Admin { get { return admin; } set { admin = value; } }
}
