using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using Riddlersoft.Graphics.Particals;
using Riddlersoft.Graphics.Particals.Emitters;
using Riddlersoft.Graphics.Particals.Modifyers;
using Riddlersoft.Graphics.Shaders;

using Riddlersoft.Graphics.Shaders._2D;
namespace Test
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D bg;

        ParticalEffect pEffect;
        ParticalEffect pEffect2;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;
            base.Initialize();

        }

        Lighting2D lighteffect;

        KillModifyer killme;

        // ==================== build the effects ===============================

        public ParticalEffect BuildDustEffect(ContentManager content)
        {
            ParticalEffect pe = new ParticalEffect();
            RectangleEmitter falingLeaves = new RectangleEmitter(new Rectangle(100, 0, 1280, 720))
            {
                Initial_LifeTime = new Riddlersoft.Graphics.Particals.FloatRange(20f, 30f),
                Initial_Colour = new ColourRange(Color.White, Color.LightGray),
                Initial_Rotaiton = new FloatRange(0, MathHelper.TwoPi),
                //   Initial_Angulor_Velocity = new FloatRange(-.05f, .05f),
                //Initial_Velocity = new Vector2Range(new Vector2(-.3f, .6f), new Vector2(.3f, .2f)),
                Initial_Velocity = new Vector2Range(new Vector2(-.2f, .4f), new Vector2(-.4f, .5f)),
                Initial_Scale = new FloatRange(.2f, 1f),
                ReleaseAmount = 5,
            };
            killme = new KillModifyer()
            {
                LiveArea = new Rectangle(200, 200, 880, 320),
            };
            falingLeaves.SetTexture(Content.Load<Texture2D>("leaf"));
            falingLeaves.AddModifyer(new OsolatingModifyer());
            falingLeaves.AddModifyer(new RotatingOsolatingModifyer());
            falingLeaves.AddModifyer(new StateFadeModifyer(falingLeaves)
            {
                InitialFade = 1,
                MiddleLifeTime = .9f,
                MiddleStateFade = 1,
                EndFade = 0f,
            });
            pe.AddEmitter(falingLeaves);
            return pe;
        }

        public ParticalEffect BuildExhaustEffect(ContentManager content)
        {
            ParticalEffect pe = new ParticalEffect();
            ConeEmitter ce = new ConeEmitter(5f)
            {
                MinRadious = MathHelper.Pi - .2f,
                MaxRadious = MathHelper.Pi + .2f,
                ParticalSpeed = 2,
                ReleaseAmount = .05f,
                Function = EmmiterFunction.Explosive,
                Initial_LifeTime = new FloatRange(1, 2),
                Initial_Scale = new FloatRange(.05f, .1f),
                Initial_Angulor_Velocity = new FloatRange(-.1f, .1f),
            };
            ce.SetTexture(Content.Load<Texture2D>("Cloud003"));
            ce.AddModifyer(new LinearScaleModifyer(ce) { EndScale = 10, });
            ce.AddModifyer(new DampaningModifyer(.01f, ce) { DampenAngulorVelocity = true, AngulorDampaningAmount = .01f, });
            ce.AddModifyer(new StateFadeModifyer(ce) { InitialFade = 0, MiddleLifeTime = .1f, MiddleStateFade = .3f, EndFade = 0f, });
            ce.AddModifyer(new LinearGravityModifyer(ce) { Gravity = new Vector2(0, -1f), });
            pe.AddEmitter(ce);
            return pe;
        }

        public ParticalEffect BuildExplostionEffec(ContentManager content)
        {
            ParticalEffect pe = new ParticalEffect();
            CircleEmitter flash = new CircleEmitter(5);
            flash.SetTexture(content.Load<Texture2D>("Particle005"));
            flash.ReleaseAmount = 1f;

            flash.ParticalSpeed = 0;
            flash.Function = EmmiterFunction.Explosive;
            flash.Initial_Velocity = new Vector2Range(Vector2.Zero, Vector2.Zero);
            flash.Initial_Angulor_Velocity = new FloatRange(-.1f, .1f);
            flash.Initial_LifeTime = new FloatRange(.1f, .1f);
            flash.Initial_Scale = new FloatRange(.1f, .1f);
            flash.Initial_Colour = new ColourRange(Color.LightYellow, Color.LightYellow);
            flash.AddModifyer(new LinearScaleModifyer(flash) { EndScale = 80 });
            pe.AddEmitter(flash);

            CircleEmitter ce = new CircleEmitter(5);
            ce.SetTexture(content.Load<Texture2D>("Particle005"));
            ce.ReleaseAmount = 50f;
            ce.ParticalSpeed = new FloatRange(.2f, 2.4f);
            ce.Function = EmmiterFunction.Explosive;
            ce.Initial_Velocity = new Vector2Range(Vector2.Zero, Vector2.Zero);
            ce.Initial_Angulor_Velocity = new FloatRange(-.1f, .1f);
            ce.Initial_LifeTime = new FloatRange(1f, 1.5f);
            ce.Initial_Scale = new FloatRange(.2f, .4f);
            ce.Initial_Colour = new ColourRange(Color.Yellow, Color.Yellow);
            ce.AddModifyer(new LinearFadeModifyer(ce) { EndFade = 0f, InitialFade = 1f });
            ce.AddModifyer(new LinearScaleModifyer(ce) { EndScale = 5 });
            ce.AddModifyer(new DampaningModifyer(.01f, ce) { DampenAngulorVelocity = true, AngulorDampaningAmount = .01f });
            pe.AddEmitter(ce);
            return pe;
        }

        public ParticalEffect BuildExplostionCloud(ContentManager content)
        {
            ParticalEffect pe = new ParticalEffect();
            CircleEmitter ce = new CircleEmitter(5);
            ce.SetTexture(content.Load<Texture2D>("Cloud003"));
            ce.ReleaseAmount = 10f;
            ce.ParticalSpeed = new FloatRange(.2f, 1.5f);
            ce.Function = EmmiterFunction.Explosive;
            ce.Initial_Velocity = new Vector2Range(Vector2.Zero, Vector2.Zero);
            ce.Initial_Angulor_Velocity = new FloatRange(-.1f, .1f);
            ce.Initial_LifeTime = new FloatRange(3f, 5f);
            ce.Initial_Scale = new FloatRange(.2f, .4f);
            ce.Initial_Colour = new ColourRange(Color.Black, new Color(25, 25, 25));
            ce.AddModifyer(new LinearFadeModifyer(ce) { EndFade = 0f, InitialFade = 1f });
            ce.AddModifyer(new LinearScaleModifyer(ce) { EndScale = 7 });
            ce.AddModifyer(new DampaningModifyer(.01f, ce) { DampenAngulorVelocity = true, AngulorDampaningAmount = .01f });
            pe.AddEmitter(ce);
            return pe;
        }

        public ParticalEffect BuildClockWorkCloud(ContentManager content)
        {
            ParticalEffect pe = new ParticalEffect();
            ConeEmitter ce = new ConeEmitter(5f)
            {
                MinRadious = MathHelper.PiOver2 - .2f,
                MaxRadious = MathHelper.PiOver2 + .2f,
                ParticalSpeed = 2,
                ReleaseAmount = .05f, //1 would be no delay 0 would never release
                Function = EmmiterFunction.Explosive,
                Initial_LifeTime = new FloatRange(1, 2),
                Initial_Scale = new FloatRange(.1f, .2f),
                Initial_Angulor_Velocity = new FloatRange(-.1f, .1f),
            };
            ce.SetTexture(content.Load<Texture2D>("Cloud003"));
            ce.AddModifyer(new LinearScaleModifyer(ce) { EndScale = 10, });
            ce.AddModifyer(new DampaningModifyer(.01f, ce) { DampenAngulorVelocity = true, AngulorDampaningAmount = .01f, });
            ce.AddModifyer(new StateFadeModifyer(ce) { InitialFade = 0, MiddleLifeTime = .1f, MiddleStateFade = .3f, EndFade = 0f, });
            ce.AddModifyer(new LinearGravityModifyer(ce) { Gravity = new Vector2(0, -1f), });
            pe.AddEmitter(ce);
            return pe;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            bg = Content.Load<Texture2D>("foreset2");
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            pEffect = BuildClockWorkCloud(Content); //this loades in an effect
            pEffect2 = BuildClockWorkCloud(Content); //this loads in a second effect

            lighteffect = Lighting2D.Load(Content);


            //lighteffect.Parameters["Brightness"].SetValue(0f);
            IsMouseVisible = true;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        MouseState ms;
        MouseState lms;
        float timelaps;
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            lms = ms;
            ms = Mouse.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timelaps += dt;

            if (ms.LeftButton == ButtonState.Pressed && lms.LeftButton == ButtonState.Released) //this will only trigger once per click
            {

                pEffect.Trigger(new Point(ms.Position.X, ms.Position.Y), 1);
            }

            if (ms.RightButton == ButtonState.Pressed) //this will keep trigger
            {
                pEffect2.Trigger(new Point(ms.Position.X, ms.Position.Y), 1);
            }


            pEffect.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            pEffect2.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            //Window.Title = pEffect.Emitters[0].Particals.Count.ToString();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);



            spriteBatch.Begin();
            spriteBatch.Draw(bg, Vector2.Zero, Color.White);
            pEffect2.Render(spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);

            pEffect.Render(spriteBatch);
            spriteBatch.End();



            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }


    }
}