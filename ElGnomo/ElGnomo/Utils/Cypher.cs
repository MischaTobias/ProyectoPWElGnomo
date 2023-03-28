namespace ElGnomo.Utils;

public static class Cypher
{
    public static string CypherText(string text)
    {
        try
        {
            var dataBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(dataBytes);
        }
        catch (Exception ex)
        {
            throw new Exception("Error in base64Encode" + ex.Message);
        }
    }
}
