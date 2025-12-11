using Dalamud.Interface.Textures;
using Dalamud.Interface.Textures.TextureWraps;
using System;


namespace BetterTeleportPlugin;



public class IconProperties
{
    public required IDalamudTextureWrap Texture { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public float U0 { get; set; }
    public float V0 { get; set; }
    public float U1 { get; set; }
    public float V1 { get; set; }
}

public class Icons
{
    

    public static IconProperties? ULDSprite(IDalamudTextureWrap tex, int x, int y, int width, int height)
    {
        float U0 = (float) x / tex.Width;
        float V0 = (float) y / tex.Height;
        float U1 = (float) (x + width) / tex.Width;
        float V1 = (float) (y + height) / tex.Height;

        IconProperties iconProperties = new IconProperties { Texture = tex, Width = width, Height = height, U0 = U0, V0 = V0, U1 = U1, V1 = V1 };
        if (iconProperties != null)
        {
            return iconProperties;
        }
        else
        {
            return null;
        }
    }

    public static ISharedImmediateTexture? GetTextureFromULDPath(string path)
    {
        var image = BetterTeleport.TextureProvider.GetFromGame(path);
        if (image != null)
        {
            return image;
        }
        else
        {
            return null;
        }
    }
    public static ISharedImmediateTexture? GetTextureFromIconID(int id)
    {
        var image = BetterTeleport.TextureProvider.GetFromGameIcon(id);
        if (image != null)
        {
            return image;
        }
        else
        {
            return null;
        }
    }

    public static IDalamudTextureWrap? ConvertToTextureWrap(ISharedImmediateTexture tex)
    {
        tex.TryGetWrap(out var wrap, out Exception? exception);
        if (tex != null && wrap != null)
        {
            return wrap;
        }
        else
        {
            return null;
        }
    }

    public static ULDLibraryData GetLocationIconData(uint id)
    {
        var texture = BetterTeleport.TeleportTexture;
        if (!IconData.IconLookup.TryGetValue(id, out var icon))
            icon = IconData.ULDLibrary.MiscLocationTableIcon;
        return icon;
    }

    public IconProperties? CreateTableIcon(uint iconID, uint id, ULDLibraryData iconData, ISharedImmediateTexture texture)
    {
        if (iconID == id)
        {
            var sprite = ULDSprite(ConvertToTextureWrap(texture), iconData.X, iconData.Y, iconData.Width, iconData.Height);
            if(sprite != null)
            {
                return sprite;
            }
            return null;
        }
        else
        {
            return null;
        }
    }
}





