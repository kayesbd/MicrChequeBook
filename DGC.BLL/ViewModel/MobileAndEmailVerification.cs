namespace KBZ.BLL.ViewModel
{
    public class MobileAndEmailVerification
    {
        public long UserId { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool IsMobileNumberVerified { get; set; }
    }
}