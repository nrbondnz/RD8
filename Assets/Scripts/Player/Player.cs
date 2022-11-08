public class Player
{
    private bool _isJumpButtonPressed = false;
    private bool _isGrounded = true;
    private bool _goingForwards = true;

    public bool IsJumpButtonPressed
    {
        get => _isJumpButtonPressed;
        set => _isJumpButtonPressed = value;
    }

    public bool IsGrounded
    {
        get => _isGrounded;
        set => _isGrounded = value;
    }

    public bool GoingForwards
    {
        get => _goingForwards;
        set => _goingForwards = value;
    }
}