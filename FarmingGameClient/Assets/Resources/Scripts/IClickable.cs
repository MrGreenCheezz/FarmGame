public enum IClickableType
{
    Plant,
    PlantPot,
    Another
}

public interface IClickable
{
   public void LeftClick();
   public void RightClick();
    public IClickableType GetClickType();
}
