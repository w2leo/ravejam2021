using System;

public enum HeightLevel {R_UNDEFINED = -1, R0_DEEP_OCEAN, R1,  R2_OCEAN, R3_COAST, R4_PLAIN, R5_HILLS, R6_MOUNTAINS, R7, R8_EVEREST }
public enum TemperatureLevel { G_UNDEFINED = -1, G0_DEATH_TEMP, G1, G2_COLD_LIFE_LOW, G3_COLD, G4_BEST, G5_WARM, G6_HEAT, G7, G8_HELL }
public enum HumidityLevel { B_UNDEFINED = -1, B0_OCEAN_OF_WATER, B1, B2_JUNGLE, B3_RESORT, B4_NORMAL_CLIMAT, B5_DRY_CLIMATE, B6_STEPPE, B7, B8_DESERT }


[Serializable] public struct LandscapeCode
{
    private const int HeightHighest = (int)HeightLevel.R8_EVEREST;
    private const int HeightLowest = (int)HeightLevel.R0_DEEP_OCEAN;
    private const int HeightLowGap = (int)HeightLevel.R1;
    private const int HeightHighGap = (int)HeightLevel.R7;

    private const int TemperatureHighest = (int)TemperatureLevel.G8_HELL;
    private const int TemperatureLowest = (int)TemperatureLevel.G0_DEATH_TEMP;
    private const int TemperatureLowGap = (int)TemperatureLevel.G1;
    private const int TemperatureHighGap = (int)TemperatureLevel.G7;

    private const int HumidityHighest = (int)HumidityLevel.B8_DESERT;
    private const int HumidityLowest = (int)HumidityLevel.B0_OCEAN_OF_WATER;
    private const int HumidityLowGap = (int)HumidityLevel.B1;
    private const int HumidityHighGap = (int)HumidityLevel.B7;

    public HeightLevel R;
    public TemperatureLevel G;
    public HumidityLevel B;

    public LandscapeCode(LandscapeCode h)
    {
        R = h.R;
        G = h.G;
        B = h.B;
    }
    
    public LandscapeCode(int R, int G, int B)
    {
        this.R = HeightLevelFromInt(R);
        this.G = TemperatureLevelFromInt(G);
        this.B = HumidityLevelFromInt(B);     
    }

    public static HeightLevel HeightLevelFromInt(int value)
    {
        return (HeightLevel)ClampLevel(value, HeightLowest, HeightHighest, HeightLowGap, HeightHighGap);
    }
    public static TemperatureLevel TemperatureLevelFromInt(int value)
    {
        return (TemperatureLevel)ClampLevel(value, TemperatureLowest, TemperatureHighest, TemperatureLowGap, TemperatureHighGap);
    }
    public static HumidityLevel HumidityLevelFromInt(int value)
    {
        return (HumidityLevel)ClampLevel(value, HumidityLowest, HumidityHighest, HumidityLowGap, HumidityHighGap);
    }

    private static int ClampLevel(int value, int lowest, int highest, int lowGap, int highGap)
    {
        if (value == -1) return value; //for Undefined Tiles. MayBe delete Later (fill by 0 or 8)
        int result = value;
        result = result == lowGap ? result + 1 : result;
        result = result == highGap ? result - 1 : result;
        return Math.Min(highest, Math.Max(lowest, result));
    }

    public LandscapeCode(HeightLevel R, TemperatureLevel G, HumidityLevel B)
    {
        this.R = R;
        this.G = G;
        this.B = B;
    }

    public static LandscapeCode operator +(LandscapeCode a, LandscapeCode b)
    {
        int tmpR, tmpG, tmpB;
        tmpR = (int)a.R + (int)b.R;
        tmpG = (int)a.G + (int)b.G;
        tmpB = (int)a.B + (int)b.B;
        return new LandscapeCode(tmpR, tmpG, tmpB);
    }

    public static LandscapeCode operator * (LandscapeCode a, int b)
        => new LandscapeCode((int)a.R * b, (int)a.G * b, (int)a.B * b);

    public static LandscapeCode operator / (LandscapeCode a, int b)
        => new LandscapeCode((int)a.R / b, (int)a.G / b, (int)a.B / b);

    public static LandscapeCode operator / (LandscapeCode a, float b)
        => new LandscapeCode(
            (int)((float)a.R / b),
            (int)((float)a.G / b), 
            (int)((float)a.B / b)
        );

    public static bool operator == (LandscapeCode a, LandscapeCode b) => a.R==b.R && a.G==b.G && a.B==b.B;

    public static bool operator != (LandscapeCode a, LandscapeCode b) => !(a==b);

    public string PrintCode()
    {
        return $"R{(int)R}G{(int)G}B{(int)B}";
    }

    public override bool Equals(object obj)
    {
        if (!(obj is LandscapeCode))
        {
            return false;
        }

        var code = (LandscapeCode)obj;
        return R == code.R &&
               G == code.G &&
               B == code.B;
    }

    public override int GetHashCode()
    {
        var hashCode = -1520100960;
        hashCode = hashCode * -1521134295 + R.GetHashCode();
        hashCode = hashCode * -1521134295 + G.GetHashCode();
        hashCode = hashCode * -1521134295 + B.GetHashCode();
        return hashCode;
    }
};