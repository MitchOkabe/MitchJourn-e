﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MitchJourn_e.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.5.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(".25")]
        public string ImagePromptWeight {
            get {
                return ((string)(this["ImagePromptWeight"]));
            }
            set {
                this["ImagePromptWeight"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public string Iter {
            get {
                return ((string)(this["Iter"]));
            }
            set {
                this["Iter"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("512")]
        public string Width {
            get {
                return ((string)(this["Width"]));
            }
            set {
                this["Width"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("512")]
        public string Height {
            get {
                return ((string)(this["Height"]));
            }
            set {
                this["Height"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("50")]
        public string Steps {
            get {
                return ((string)(this["Steps"]));
            }
            set {
                this["Steps"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1337")]
        public string Seed {
            get {
                return ((string)(this["Seed"]));
            }
            set {
                this["Seed"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("7.5")]
        public string Scale {
            get {
                return ((string)(this["Scale"]));
            }
            set {
                this["Scale"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\invokeai\\")]
        public string MainPath {
            get {
                return ((string)(this["MainPath"]));
            }
            set {
                this["MainPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(".venv\\scripts\\txt2img.py")]
        public string TextToImagePath {
            get {
                return ((string)(this["TextToImagePath"]));
            }
            set {
                this["TextToImagePath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(".venv\\scripts\\img2img.py")]
        public string ImageToImagePath {
            get {
                return ((string)(this["ImageToImagePath"]));
            }
            set {
                this["ImageToImagePath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("configs\\stable-diffusion\\v1-inference.yaml")]
        public string ConfigPath {
            get {
                return ((string)(this["ConfigPath"]));
            }
            set {
                this["ConfigPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("models\\ldm\\stable-diffusion-v1\\model.ckpt")]
        public string ModelPath {
            get {
                return ((string)(this["ModelPath"]));
            }
            set {
                this["ModelPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public string SlowMode {
            get {
                return ((string)(this["SlowMode"]));
            }
            set {
                this["SlowMode"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(".5")]
        public string gfpganScale {
            get {
                return ((string)(this["gfpganScale"]));
            }
            set {
                this["gfpganScale"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2")]
        public string gfpganUprezScale {
            get {
                return ((string)(this["gfpganUprezScale"]));
            }
            set {
                this["gfpganUprezScale"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("512")]
        public string gfpganBgTileSize {
            get {
                return ((string)(this["gfpganBgTileSize"]));
            }
            set {
                this["gfpganBgTileSize"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("k_euler_a")]
        public string SamplerType {
            get {
                return ((string)(this["SamplerType"]));
            }
            set {
                this["SamplerType"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public string UseFullPrecision {
            get {
                return ((string)(this["UseFullPrecision"]));
            }
            set {
                this["UseFullPrecision"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("512:512")]
        public string AspectRatio11 {
            get {
                return ((string)(this["AspectRatio11"]));
            }
            set {
                this["AspectRatio11"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("576:384")]
        public string AspectRatio32 {
            get {
                return ((string)(this["AspectRatio32"]));
            }
            set {
                this["AspectRatio32"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("640:384")]
        public string AspectRatio169 {
            get {
                return ((string)(this["AspectRatio169"]));
            }
            set {
                this["AspectRatio169"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("384:576")]
        public string AspectRatio23 {
            get {
                return ((string)(this["AspectRatio23"]));
            }
            set {
                this["AspectRatio23"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("256:576")]
        public string AspectRatio920 {
            get {
                return ((string)(this["AspectRatio920"]));
            }
            set {
                this["AspectRatio920"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public string EnableWelcomePrompt {
            get {
                return ((string)(this["EnableWelcomePrompt"]));
            }
            set {
                this["EnableWelcomePrompt"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsd=\"http://www.w3." +
            "org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">\r\n  <s" +
            "tring>Artists/Greg Rutkowski (dark concept art)=by greg rutkowski</string>\r\n  <s" +
            "tring>Artists/Thoma Kinkades (Flowery Landscape)=by thomas kinkade</string>\r\n  <" +
            "string>Artists/John Berkey (Detailed paintings)=very detailed painting by John B" +
            "erkey</string>\r\n  <string>Artists/William-Adolphe Bouguereau (Old style painting" +
            ")=painting by William-Adolphe Bouguereau</string>\r\n  <string>Artists/Claude Mone" +
            "t (Noisy painting)=painting by Claude Monet</string>\r\n  <string>Artists/Ernest Z" +
            "acharevic (Wall mural)=wall mural by Ernest Zacharevic</string>\r\n  <string>Artis" +
            "ts/Stephan Martiniere (sci-fi concept art)=by Stephan Martiniere</string>\r\n  <st" +
            "ring>Artists/Carl Barks (Cartoonist)=cartoon by Carl Barks</string>\r\n  <string>A" +
            "rtists/Peter Gric (Creepy drawings)=Peter Gric print</string>\r\n  <string>Artists" +
            "/Sung Choi (Sci-Fi fantacy concept art)=trending on artstation by sung choi</str" +
            "ing>\r\n  <string>Artists/Ilya Kuvshinov (Retro anime portraits)=by ilya kuvshinov" +
            "</string>\r\n  <string>Artists/Andreas Rocha (Fantacy landscapes)=by andreas rocha" +
            "</string>\r\n  <string>Artists/Lois Van Baarle (Paited portraits)=by lois van baar" +
            "le</string>\r\n  <string>Artists/Rossdraws (Digital art portraits)=by rossdraws</s" +
            "tring>\r\n  <string>Artists/Rembrandt (Baroque paintings)=by Rembrandt</string>\r\n " +
            " <string>Artists/Marc Simonetti (Detailed dark digital landscapes)=highly detail" +
            "ed by marc simonetti</string>\r\n  <string>Artists/Luis Royo (Creepy detailed port" +
            "raits)=by Luis Royo</string>\r\n  <string>Artists/Beksiński (Creepy abstract paint" +
            "ings)=by beksiński</string>\r\n  <string>Artists/Hieronymus Bosch (Renaissance pai" +
            "ntings)=by hieronymus bosch</string>\r\n  <string>Styles/Midjourney1=Splash art, l" +
            "ight dust, magnificent, details, sharp focus, intricate, beautiful, triadic cont" +
            "rast colors, trending artstation, pixiv, digital art</string>\r\n  <string>Styles/" +
            "Midjourney2=Splash art light dust trending Professional majestic oil painting of" +
            " establishing shot by Ed Blinkey and Atey Ghailan and Studio Ghibli and Jeremy M" +
            "ann and Greg Manchess and Antonio Moro volumetric lighting, dramatic lighting</s" +
            "tring>\r\n  <string>Styles/Photo=f/1.4 50mm 200iso 4k</string>\r\n  <string>Styles/i" +
            "Phone photo=iPhone photo</string>\r\n  <string>Styles/Portrait Photography1=facial" +
            " asymmetry, striking features, tack sharp, fine-art photography, 180mm f/1.8 200" +
            "iso</string>\r\n  <string>Styles/Stability Portrait=ultrarealistic uhd faces, Koda" +
            "k Ultramax 800, pexels, 85mm, casual pose, 35mm film roll photo, hard light, det" +
            "ailed skin texture, masterpiece, sharp focus, pretty, lovely, adorale, attractiv" +
            "e, hasselblad, candid street podrait</string>\r\n  <string>Styles/Landscape=octane" +
            " render 4k unreal engine cryengine 200iso dynamic range hdr wonderous awesome gr" +
            "eg rutkowski sung choi thomas kinkade vincent van gogh</string>\r\n  <string>Style" +
            "s/Stability Landscape1=unsplash contest winner, breath-taking beautiful, warm sh" +
            "ades of blue, video still</string>\r\n  <string>Styles/High Quality=hyperdetailed " +
            "hd 4k 8k sharp focus highly detailed uhd</string>\r\n  <string>Styles/Elegant Port" +
            "rait=portrait photo headshot by mucha, sharp focus, elegant, render, octane, det" +
            "ailed, award winning photography, masterpiece, rim lit</string>\r\n  <string>Style" +
            "s/Artistic Portrait=a vibrant professional studio portrait photography casual, d" +
            "elightful, intricate, piercing eyes, nouveau, curated collection, annie leibovit" +
            "z, nikon, award winning, breathtaking, groundbreaking, superb, outstanding, lens" +
            "culture portrait awards, photoshopped, dramatic lighting, 8k</string>\r\n  <string" +
            ">Styles/Rendered Portrait=soft, octane render, unreal engine, photograph, realis" +
            "tic skin texture, photorealistic, hyper realism, highly detailed, 85mm portrait " +
            "photography, award winning, hard rim lighting photography</string>\r\n  <string>St" +
            "yles/Stary Portrait=a portrait with a luminous clothing, eyes shut, mouth closed" +
            ", wind, sky, clouds, the moon, moonlight, stars, universe, fireflies, butterflie" +
            "s, lights, lens flares effects, swirly bokeh, brush effect, In style of Yoji Shi" +
            "nkawa, Jackson Pollock, wojtek fus, by Makoto Shinkai, concept art, celestial, a" +
            "mazing, astonishing, wonderful, beautiful, highly detailed, centered</string>\r\n " +
            " <string>Styles/Photobashing=photobashing</string>\r\n  <string>Styles/Algorithmic" +
            " Art=algorithmic art</string>\r\n  <string>Styles/Stars and Galaxies=composed of b" +
            "illions of stars, digital art</string>\r\n  <string>Styles/Cartoon dramatic style=" +
            "digital art, 2.5D style</string>\r\n  <string>Styles/Made of smoke=made of very de" +
            "tailed curling wispy glowing multicolored smoke, digital art, volumetric, 3D ren" +
            "der, Octane render</string>\r\n  <string>Styles/Detailed Particles=detailed partic" +
            "le, digital art</string>\r\n  <string>Styles/Mixed Media=mixed media</string>\r\n  <" +
            "string>Styles/Magical world=magical world</string>\r\n  <string>Styles/concept art" +
            "=concept art splash art</string>\r\n  <string>Styles/Stability Architecture=A Hype" +
            "rrealistic photograph of German architectural building, lens flares, cinematic, " +
            "hdri, matte painting, concept art, celestial, soft render, highly detailed, cgso" +
            "ciety, octane render, architectural HD, HQ, 4k, 8k</string>\r\n  <string>Cinematog" +
            "raphy/Cinematic lighting=cinematic lighting</string>\r\n  <string>Cinematography/L" +
            "ow angle=photograph, taken from a low angle</string>\r\n  <string>Cinematography/O" +
            "ver the shoulder=photograph, over-the-shoulder</string>\r\n  <string>Cinematograph" +
            "y/Drone footage=photograph taken from a drone</string>\r\n  <string>Cinematography" +
            "/Close up=close up</string>\r\n  <string>Cinematography/Wide angle=wide angle</str" +
            "ing>\r\n  <string>Cinematography/Movie ccene=anamorphic lens film scene movie stil" +
            "l f/2 800iso 35mm light dust haze</string>\r\n  <string>Cinematography/Golden hour" +
            "=golden hour</string>\r\n  <string>Cinematography/beautiful lighting=beautiful lig" +
            "hting</string>\r\n  <string>Sources/artstation=artstation</string>\r\n  <string>Sour" +
            "ces/instagram=instagram</string>\r\n  <string>Sources/deviantart=deviantart</strin" +
            "g>\r\n  <string>Sources/reddit=reddit</string>\r\n  <string>Sources/shutterstock=shu" +
            "tterstock</string>\r\n  <string>Sources/tumblr=tumblr</string>\r\n  <string>Sources/" +
            "cgsociety=cgsociety</string>\r\n  <string>Sources/flickr=flickr</string>\r\n  <strin" +
            "g>Sources/behance=behance</string>\r\n  <string>Sources/dribble=dribble</string>\r\n" +
            "  <string>Sources/pexels=pexels</string>\r\n  <string>Sources/pinterest=pinterest<" +
            "/string>\r\n  <string>Sources/pixabay=pixabay</string>\r\n  <string>Sources/pixiv=pi" +
            "xiv</string>\r\n  <string>Sources/polycount=polycount</string>\r\n  <string>Effects/" +
            "post processing=post processing</string>\r\n  <string>Effects/cgi=cgi</string>\r\n  " +
            "<string>Effects/chromatic aberration=chromatic aberration</string>\r\n  <string>Ef" +
            "fects/anaglyph=anaglyph</string>\r\n  <string>Effects/cropped=cropped</string>\r\n  " +
            "<string>Effects/glowing edges=glowing edges</string>\r\n  <string>Effects/glow eff" +
            "ect=glow effect</string>\r\n  <string>Effects/bokeh=bokeh</string>\r\n  <string>Effe" +
            "cts/dramatic lighting=dramatic lighting</string>\r\n  <string>Effects/soft lightin" +
            "g=soft lighting</string>\r\n  <string>Effects/hard lighting=hard lighting</string>" +
            "\r\n  <string>Effects/glamor shot=glamor shot</string>\r\n  <string>Effects/colourfu" +
            "l=colourful</string>\r\n  <string>Effects/complimentary-colours=complimentary-colo" +
            "urs</string>\r\n  <string>Effects/dark mood=dark mood</string>\r\n  <string>Effects/" +
            "multiverse=multiverse</string>\r\n  <string>Effects/volumetric lighting=volumetric" +
            " lighting</string>\r\n  <string>Effects/lumen global illumination=lumen global ill" +
            "umination</string>\r\n  <string>Effects/octane render=octane render</string>\r\n  <s" +
            "tring>Effects/atmospheric=atmospheric</string>\r\n  <string>Effects/technicolour=t" +
            "echnicolour</string>\r\n  <string>Mediums/photo realistic=photo realistic</string>" +
            "\r\n  <string>Mediums/anime=anime manga</string>\r\n  <string>Mediums/graphic novel=" +
            "graphic novel</string>\r\n  <string>Mediums/fountain pen=fountain pen</string>\r\n  " +
            "<string>Mediums/pastel art=pastel art</string>\r\n  <string>Mediums/fine art=fine " +
            "art</string>\r\n  <string>Mediums/acrylic paint=acrylic paint</string>\r\n  <string>" +
            "Mediums/oil paint=oil paint</string>\r\n  <string>Mediums/watercolour=watercolour<" +
            "/string>\r\n  <string>Mediums/digital art=digital art</string>\r\n  <string>Mediums/" +
            "magazine=magazine</string>\r\n  <string>Mediums/comic book=comic book</string>\r\n  " +
            "<string>Mediums/pokemon card=pokemon card</string>\r\n  <string>Mediums/puzzle=puz" +
            "zle</string>\r\n  <string>Mediums/logo=logo</string>\r\n  <string>Mediums/editorial " +
            "photography=editorial photography</string>\r\n  <string>Mediums/wildlife photograp" +
            "hy=wildlife photography</string>\r\n  <string>Negative/Photo=cartoon anime art pai" +
            "nting ugly</string>\r\n  <string>Negative/Portrait=bad anatomy bad proportions blu" +
            "rry cloned face deformed disfigured duplicate gross proportions long neck mutati" +
            "on mutilated morbid out of frame poorly drawn face</string>\r\n  <string>Negative/" +
            "Hands=poorly drawn hands extra arms extra fingers extra limbs extra legs too man" +
            "y fingers fused fingers malformed limbs missing arms missmg legs mutated hands</" +
            "string>\r\n  <string>Negative/Safety=nsfw nude naked</string>\r\n  <string>Negative/" +
            "Landscape=portrait person people text signature watermark frame framed display</" +
            "string>\r\n  <string>Negative/High Quality=lowres text error cropped worst quality" +
            " low quality normal quality jpeg artifacts signature watermark username blurry a" +
            "rtist name deformed disfigured poorly drawn out of focus censorship amateur draw" +
            "ing bad art poor art messy drawing</string>\r\n  <string>Negative/Stability Portra" +
            "it=fox in a lab coat, extra limb, from scene from twin peaks, brutalist futurist" +
            "ic interior, retro futurism, dramatic nautical scene , ornate hospital room, cru" +
            "mbling masonry, pale blue armor, mechanical paw, laser guns, pulp sci fi, two de" +
            "er wearing suits</string>\r\n  <string>Negative/Stability Portrait2=colourful 3d c" +
            "rystals and gems, vintage 1950s stamp, full color manga cover, kewpie, two girls" +
            ", anime, fairytale illustration, chinese ribbon dance, children illustration, fl" +
            "ower dress, illustration, silk shoes, classic children\'s illustrations, adorable" +
            " and whimsical</string>\r\n  <string>Negative/Stability Portrait3=blender, cropped" +
            ", lowres, poorly drawn face, out of frame, poorly drawn hands, double, blurred, " +
            "disfigured, deformed, repetitive, black and white</string>\r\n  <string>Negative/S" +
            "tability Landscape=wearing victorian brass goggles, alien, alien isolation, ink " +
            "on paper, flash sheet, robot barkeep, black micron pen illustration, black banda" +
            "ge on arms, mcbess, grid of eye shapes, moscow metro, b&amp;w, childish, steelpu" +
            "nk, holding a cigar, dark show room, dieselpunk, necromancer, mouse face</string" +
            ">\r\n  <string>Negative/Stability Digital Art=autumn rain turkel, two finnish lapp" +
            "hundsv 2d lasercut earrings tribal dance, risograph, white orchids, egyptian sum" +
            "erian features, large temples, childish</string>\r\n  <string>Negative/Stability D" +
            "igital Art2=tintype photograph, moth inspired dress, red on black, lace dress, d" +
            "eformed, 1970 film photography, very sexy woman with black hair, fashion model p" +
            "ortrait, alien faces, animal photography, disfigured</string>\r\n  <string>Negativ" +
            "e/Stability Sci-Fi Art=trees in foreground, ink pen sketch, on a velvet tableclo" +
            "th, black ink on textured paper, long black straight hair, pencil skit pink lips" +
            ", sophisticated hands, noir, mystic winter landscape, linocut print, sailboats, " +
            "watercolor strokes, grass landscape, pink rose, vintage 1950s stamp</string>\r\n  " +
            "<string>Negative/Stability Sci-Fi Render=1980s flower power hippy, impressionist" +
            " watercolor, sitting at the beach, hot pink, german expressionist woodcut, weari" +
            "ng in stocking, pen drawing, young woman, on a branch, watercolour on paper, gre" +
            "en and pink, wearing shades, in a garden, drinking their hearts out, old</string" +
            ">\r\n  <string>Negative/Stability Comic=lofi bioshock steampunk portrait, 4k digit" +
            "al painting, tapir, female portrait with flowers, metamorphosis complex 3d rende" +
            "r, white petal, skyrim screenshot, fine art fashion photography, side portrait o" +
            "fa girl, lotus flower, lost place photo, black and white, text, title</string>\r\n" +
            "  <string>Negative/Stability Comic2=I elderly greek goddess, 1900s photograph, l" +
            "ong glowing ethereal hair, gorgeous face, brown hair!, calotype, sheep wool, ham" +
            "mershoi, eyelashes, dreamy and ethereal, single pine, nsfw, white lilies, flower" +
            "s on hair</string>\r\n  <string>Negative/Stability Fantacy=troops searching the ar" +
            "ea, from police academy 2 (1985), gunma prefecture, concrete housing, nsfw, blur" +
            ", wood block print, in africa, kodak tri - x 3 5 mm, black lace, american flag, " +
            "football players, tokyojapan, ferrari 458, woodblock print</string>\r\n  <string>N" +
            "egative/Stability Cyberpunk=purple and red flowers, wood block print, idyllic an" +
            "d fruitful land, standing on a ladder, postman pat, gnome druid, botanic waterco" +
            "lors, aged paper, standing with a parasol, she has a crown of dried flowers, wea" +
            "ring an apron, pink door, nature journal, theodor kittelsen, harvest fall vibran" +
            "cy, crop</string>\r\n  <string>Negative/Stability Cars=blue sky and white clouds, " +
            "gourds, 1880 photograph, troops searching the area, b&amp;w, rolling green hills" +
            ", aboriginal engraving, arge black hat, spiral lines, several cottages, with anc" +
            "hor man and woman, ink drawing, brown hair, wide greenways, black ink on white p" +
            "aper</string>\r\n  <string>Negative/Stability Texture=fox in a lab coat, extra lim" +
            "b, from scene from twin peaks, brutalist futuristic interior, retro futurism, dr" +
            "amatic nautical scene, ornate hospital room, crumbling masonry, pale blue armor," +
            " mechanical paw, laser guns, pulp sci fi, two deer wearing suits</string>\r\n  <st" +
            "ring>Negative/Stability Food=warframe hound art, black ink on white paper, art n" +
            "ouveau ink illustration, huge feathery wings, holding a tattered magical book, a" +
            "nubis, lots of furniture, clockwork steampunk, low quality, childish, mecha suit" +
            ", blue moonlight, many mechflowers, fancy clouds, crazy hacker girl, blue flames" +
            ", solarpunk, undersea temple, in a lavender field in france : -5</string>\r\n  <st" +
            "ring>Negative/Stability Food2=fantasy dungeon, beautiful woman pot-trait, on sti" +
            "lts, tribal dance, 3d high definition, cell shaded cartoon, dozens of jeweled ne" +
            "cklaces, disco lights, in full military garb, long neck, rainbow aura crystals, " +
            "full moon lighting, patterned visionary art, laser guns, mystic winter landscape" +
            ", feathered arrows, mirror selfie</string>\r\n  <string>Negative/Stability Charact" +
            "er=dramatic space battle, engraving from 1700s, spinosaurus, illegible rosicruci" +
            "an symbols, medieval woodcut, elegant evening gowns!, battle of 1453, black ink " +
            "on white paper, ornamental arrows, croatian coastline, lush vegetation, arabian " +
            "beauty, 1990s 1992 sega genesis box att : -5</string>\r\n  <string>Negative/Stabil" +
            "ity Character2=pen and ink doodles, blowing hair, in the foreground a small town" +
            ", white frame, soft blues and greens, capybara, long flowing hair, polish poster" +
            " an, books and flowers, fairytale illustration, cute and funny, white stockings," +
            " cropped, out of frame : -5</string>\r\n</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection PromptHelperPresets {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["PromptHelperPresets"]));
            }
            set {
                this["PromptHelperPresets"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Welcome,Test")]
        public string SortList {
            get {
                return ((string)(this["SortList"]));
            }
            set {
                this["SortList"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool EnableSortList {
            get {
                return ((bool)(this["EnableSortList"]));
            }
            set {
                this["EnableSortList"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public string SaveProgress {
            get {
                return ((string)(this["SaveProgress"]));
            }
            set {
                this["SaveProgress"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public string SafetyChecker {
            get {
                return ((string)(this["SafetyChecker"]));
            }
            set {
                this["SafetyChecker"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public string DeleteOnExit {
            get {
                return ((string)(this["DeleteOnExit"]));
            }
            set {
                this["DeleteOnExit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\InvokeAI-main\\outputs")]
        public string OutputPath {
            get {
                return ((string)(this["OutputPath"]));
            }
            set {
                this["OutputPath"] = value;
            }
        }
    }
}
