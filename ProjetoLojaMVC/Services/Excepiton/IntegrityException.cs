using System;


namespace ProjetoLojaMVC.Services.Excepiton
{
    public class IntegrityException:ApplicationException
    {
        public IntegrityException(string message): base(message)
        {

        }
    }
}
