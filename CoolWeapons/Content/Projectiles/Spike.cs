using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CoolWeapons.Content.Projectiles
{
    public class Spike : ModProjectile
    {
        public NPC targeted_npc = null;
        int frame = 0;
        int stage = 5;
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;

            Projectile.friendly = true;
            Projectile.hostile = false;

            Projectile.DamageType = DamageClass.Magic;

            Projectile.tileCollide = false;
        }

        public void PlaceRadius(int radius, int block, Vector2 position)
        {
            for (int i = -radius; i <= radius; i++)
            {
                for(int j = -radius; j <= radius; j++)
                {
                    Vector2 pos_to_place = (new Vector2(position.X + (i * 16), position.Y + (j * 16))).ToTileCoordinates().ToVector2();
                    Main.NewText(pos_to_place);
                    WorldGen.PlaceTile((int)pos_to_place.X, (int)pos_to_place.Y, block, true, true);
                }
            }
        }

        public override void AI()
        {
            if (Projectile.active)
            {
                int block = TileID.Obsidian;

                Vector2 pos = Projectile.Center;
                if (frame % 3 == 0)
                {
                    if (stage == 5)
                    {
                        PlaceRadius(3, block, pos);
                    }
                    else if (stage == 4 || stage == 3)
                    {
                        PlaceRadius(2, block, pos);
                    }
                    else if (stage == 2 || stage == 1)
                    {
                        PlaceRadius(1, block, pos);
                    }
                    else if (stage == 0)
                    {
                        Projectile.Kill();
                        Projectile.active = false;
                    }
                    stage--;
                }

                frame++;
            }
        }
    }
}
