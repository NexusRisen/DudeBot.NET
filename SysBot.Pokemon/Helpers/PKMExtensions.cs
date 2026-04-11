using PKHeX.Core;

namespace SysBot.Pokemon;

public static class PKMExtensions
{
    public static void SetHandlerandMemory(this PKM pkm, ITrainerInfo trainerInfo, IEncounterable? encounter)
    {
        pkm.CurrentHandler = 0;
        pkm.HandlingTrainerName = trainerInfo.OT;
        pkm.HandlingTrainerGender = trainerInfo.Gender;
        if (encounter != null)
        {
            pkm.MetLocation = encounter.Location;
            pkm.MetLevel = encounter.LevelMin;
        }
    }

    public static void WriteEncryptedDataStored(this PKM pkm, byte[] data)
    {
        pkm.Data.CopyTo(data);
    }

    public static void WriteEncryptedDataParty(this PKM pkm, byte[] data)
    {
        pkm.Data.CopyTo(data);
    }
}
