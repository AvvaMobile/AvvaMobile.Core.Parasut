using AvvaMobile.Core.Parasut;

var parasut = new Parasut
{
    CompanyID = "Company ID",
    Username = "Username",
    Password = "Password",
    ClientID = "Client ID",
    ClientSecret = "Client Secret"
};

#region Get Token
var getTokenResponse = await parasut.GetTokenAsync();
if (getTokenResponse.IsSuccess)
{
    Console.WriteLine("access_token: " + getTokenResponse.Data.access_token);
}
else
{
    Console.WriteLine("ERROR: " + getTokenResponse.Message);
}
#endregion


Console.ReadKey();