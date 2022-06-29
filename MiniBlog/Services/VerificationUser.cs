using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace MiniBlog.Services;

public class VerificationUser
{
    private readonly ManagerData manager;

    public VerificationUser(ManagerData manager)
    {
        this.manager = manager;
    }

    public async Task<bool> IsValidUser(LoginModel login)
    {
        var user = await manager.Users.GetUserByNicknameAsync(login.Nickname);

        if (user is null) return false;

        return login.Nickname == user.Nickname
            && EncryptPassword(login.Password) == user.Password;
    }

    public string EncryptPassword(string password)
    {
        var saltBytes = Encoding.UTF8.GetBytes(password);

        var hashBytes = KeyDerivation.Pbkdf2(
            password: password,
            salt: saltBytes,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 1000,
            numBytesRequested: 256 / 8);

        var hashText = BitConverter.ToString(hashBytes)
            .Replace(oldValue: "-",
                     newValue: string.Empty,
                     comparisonType: StringComparison.OrdinalIgnoreCase);
        return hashText;
    }
}

