namespace Pulsar.Graphics.OpenGL
{
    public class GLTexture : GPUResource<uint>, GPUTexture
    {
        public GLTexture(uint h) : base(h)
        {
        }
        
        /*public void Upload(ITextureResource tex)
        {
            throw new System.NotImplementedException();
        }*/
    }
}