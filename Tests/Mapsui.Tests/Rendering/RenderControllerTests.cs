using Mapsui.Layers;
using Mapsui.Manipulations;
using Mapsui.Rendering;
using Mapsui.Styles;
using Mapsui.Widgets;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace Mapsui.Tests.Rendering;

[TestFixture]
public sealed class RenderControllerTests
{
    [Test]
    public void Render_PreparesNonSkiaTargetBeforeRendering()
    {
        using var map = new Map();
        map.Navigator.SetSize(100, 100);
        var renderer = new RecordingMapRenderer();
        using var controller = new RenderController(() => map, static () => { }, renderer);
        var target = new object();

        controller.Render(target, 1.5f);

        Assert.That(renderer.PreparedTarget, Is.SameAs(target));
        Assert.That(renderer.RenderedTarget, Is.SameAs(target));
        Assert.That(renderer.PixelDensity, Is.EqualTo(1.5f));
        Assert.That(renderer.PrepareSequence, Is.LessThan(renderer.RenderSequence));
    }

    private sealed class RecordingMapRenderer : IMapRenderer
    {
        private int _sequence;

        public object? PreparedTarget { get; private set; }
        public object? RenderedTarget { get; private set; }
        public float PixelDensity { get; private set; }
        public int PrepareSequence { get; private set; }
        public int RenderSequence { get; private set; }

        public void PrepareRenderTarget(object target, float pixelDensity)
        {
            PreparedTarget = target;
            PixelDensity = pixelDensity;
            PrepareSequence = ++_sequence;
        }

        public void Render(
            object target,
            Viewport viewport,
            IEnumerable<ILayer> layers,
            IEnumerable<IWidget> widgets,
            RenderService renderService,
            Color? background = null,
            MRect? dirtyRegion = null,
            CoordinateSpace coordinateSpace = CoordinateSpace.World)
        {
            RenderedTarget = target;
            RenderSequence = ++_sequence;
        }

        public void UpdateDrawables(Viewport viewport, ILayer layer, RenderService renderService)
        {
        }

        public IDrawable? CreateDrawableForFeature(
            Viewport viewport,
            ILayer layer,
            IFeature feature,
            IStyle style,
            RenderService renderService)
            => null;

        public MemoryStream RenderToBitmapStream(
            Viewport viewport,
            IEnumerable<ILayer> layers,
            RenderService renderService,
            Color? background = null,
            float pixelDensity = 1,
            IEnumerable<IWidget>? widgets = null,
            RenderFormat renderFormat = RenderFormat.Png,
            int quality = 100)
            => throw new NotSupportedException();

        public bool TryGetWidgetRenderer(Type widgetType, out IWidgetRenderer? widgetRenderer)
        {
            widgetRenderer = null;
            return false;
        }

        public bool TryGetStyleRenderer(Type styleType, out IStyleRenderer? styleRenderer)
        {
            styleRenderer = null;
            return false;
        }

        public MapInfo GetMapInfo(
            ScreenPosition screenPosition,
            Viewport viewport,
            IEnumerable<ILayer> layers,
            RenderService renderService,
            int margin = 0)
            => throw new NotSupportedException();
    }
}
