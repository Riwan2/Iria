using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Iria
{
	public static class Primitive
	{
		//Line
		public static void DrawLine(SpriteBatch pSpriteBatch, int pStartX, int pStartY, int pEndX, int pEndY, Color pColor)
		{
			int xDif = pStartX - pEndX;
			int yDif = pStartY - pEndY;
			int length = (int)Math.Sqrt(xDif * xDif + yDif * yDif);
			float angle = util.getAngle(new Vector2(pEndX, pEndY).ToPoint(), new Vector2(pStartX, pStartY).ToPoint());

			pSpriteBatch.Draw(AssetManager.PointTexture,
							 new Rectangle(pStartX, pStartY, length, 1),
							 null, pColor, angle, Vector2.Zero, SpriteEffects.None, 0);
		}
		//Rectangle
		public static void DrawRectangle(SpriteBatch pSpriteBatch, int pStartX, int pStartY, int pWidth, int pHeight,
		                                 Color pColor, float layer = 0f)
		{
			pSpriteBatch.Draw(AssetManager.PointTexture, new Rectangle(pStartX, pStartY, pWidth, pHeight), null, 
			                  pColor, 0, Vector2.Zero, SpriteEffects.None, layer);
		}
		public static void DrawRectangle(SpriteBatch pSpriteBatch, Rectangle pRectangle, Color pColor, float layer = 0f)
		{
			pSpriteBatch.Draw(AssetManager.PointTexture, pRectangle, null, pColor,
							  0, Vector2.Zero, SpriteEffects.None, layer);
		}
	}
}
