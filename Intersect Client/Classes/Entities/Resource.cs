﻿using System;
using Intersect;
using Intersect.Enums;
using Intersect.GameObjects;
using IntersectClientExtras.File_Management;
using IntersectClientExtras.GenericClasses;
using IntersectClientExtras.Graphics;
using Intersect_Client.Classes.Core;
using Intersect_Client.Classes.General;
using Intersect_Client.Classes.Maps;
using Color = IntersectClientExtras.GenericClasses.Color;

namespace Intersect_Client.Classes.Entities
{
    public class Resource : Entity
    {
        public ResourceBase _baseResource;
        private bool _hasRenderBounds;
        FloatRect destRectangle = new FloatRect();
        public bool IsDead;
        FloatRect srcRectangle = new FloatRect();

        public Resource(int index, long spawnTime, ByteBuffer bf) : base(index, spawnTime, bf)
        {
        }

        public ResourceBase GetResourceBase()
        {
            return _baseResource;
        }

        public override void Load(ByteBuffer bf)
        {
            base.Load(bf);
            IsDead = Convert.ToBoolean(bf.ReadInteger());
            var baseIndex = bf.ReadInteger();
            _baseResource = ResourceBase.Lookup.Get<ResourceBase>(baseIndex);
            HideName = 1;
        }

        public override EntityTypes GetEntityType()
        {
            return EntityTypes.Resource;
        }

        public override bool Update()
        {
            CalculateRenderBounds();
            bool result = base.Update();
            if (!_hasRenderBounds)
            {
                CalculateRenderBounds();
            }
            if (result &&
                !GameGraphics.CurrentView.IntersectsWith(new FloatRect(destRectangle.Left, destRectangle.Top,
                    destRectangle.Width, destRectangle.Height)))
            {
                if (RenderList != null)
                {
                    RenderList.Remove(this);
                }
            }
            return result;
        }

        private void CalculateRenderBounds()
        {
            var map = MapInstance.Lookup.Get<MapInstance>(CurrentMap);
            if (map == null)
            {
                return;
            }
            GameTexture srcTexture = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Resource, MySprite);
            if (srcTexture != null)
            {
                srcRectangle = new FloatRect(0, 0, srcTexture.GetWidth(), srcTexture.GetHeight());
                destRectangle.Y = (int) (map.GetY() + CurrentY * Options.TileHeight + OffsetY);
                destRectangle.X = (int) (map.GetX() + CurrentX * Options.TileWidth + OffsetX);
                if (srcRectangle.Height > 32)
                {
                    destRectangle.Y -= srcRectangle.Height - 32;
                }
                if (srcRectangle.Width > 32)
                {
                    destRectangle.X -= (srcRectangle.Width - 32) / 2;
                }
                destRectangle.Width = srcRectangle.Width;
                destRectangle.Height = srcRectangle.Height;
                _hasRenderBounds = true;
            }
        }

        //Rendering Resources
        public override void Draw()
        {
            if (MapInstance.Lookup.Get<MapInstance>(CurrentMap) == null || !Globals.GridMaps.Contains(CurrentMap)) return;
            GameTexture srcTexture = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Resource, MySprite);
            if (srcTexture != null)
            {
                GameGraphics.DrawGameTexture(srcTexture, srcRectangle, destRectangle, Color.White);
            }
        }
    }
}