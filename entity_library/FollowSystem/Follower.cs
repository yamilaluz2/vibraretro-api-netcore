public class Follower
{
    private int id;
    public int ID { get { return id; } set { this.id = value; } }

    private User? followerUser;
    public User? FollowerUser { get { return followerUser; } set { this.followerUser = value; } }
    
    private User? followedUser;
    public User? FollowedUser{get{ return followedUser; } set{ this.followedUser = value; }}


}