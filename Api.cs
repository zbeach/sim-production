namespace Api
{
  class Test
  {
    static void Main()
    {
      Global.connection.Open();

      Global.connection.Close();
    }
  }
}

