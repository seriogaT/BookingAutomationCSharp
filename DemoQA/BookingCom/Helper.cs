namespace DemoQA.BookingCom;

public static class Helper
{
   public static void Wait(int seconds)
   {
      Thread.Sleep(seconds * 1000);
   }
}