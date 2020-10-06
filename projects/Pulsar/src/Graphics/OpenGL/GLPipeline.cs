using System;
using System.Runtime.InteropServices;
using Pulsar.Maths;
using Ubersharp.Client.Rendering.OpenGL;

namespace Pulsar.Graphics.OpenGL
{
    public class GLPipeline : GPUPipeline
    {
        private GLContext _context;
        private GPUResource<uint> _program;
        private GPUResource<uint> _vao;
        private GPUResource<uint>[] _buffers;
        private IntPtr[] _bufferPtrs;
        private GPUResource<uint> _indices;
        private IntPtr _indicePtr;
        private GLFramebuffer _framebuffer;
        public GLPipeline(GLContext context, GPUResource<uint> shaderProgram, GPUResource<uint> vao, GPUResource<uint>[] buffers, GPUResource<uint> indices)
        {
            _context = context;
            _program = shaderProgram;
            _vao = vao;
            _buffers = buffers;
            _indices = indices;
            _bufferPtrs = new IntPtr[buffers.Length];
            for (var i = 0; i < _bufferPtrs.Length; i++)
            {
                //_bufferPtrs[i] = Gl.glMapNamedBuffer(_buffers[i].GetHandle(), Gl.GL_WRITE_ONLY);
                //Console.WriteLine("adresse :" + _bufferPtrs[i] + "  error : " + Gl.glGetError());
            }
            //if (indices != null)
                //_indicePtr = Gl.glMapNamedBuffer(_indices.GetHandle(), Gl.GL_WRITE_ONLY);
        }
        
        public void SetUniform(string name, int value)
        {
            throw new System.NotImplementedException();
        }

        public void SetUniform(string name, float value)
        {
            throw new System.NotImplementedException();
        }

        public void SetUniform(string name, Vector2 value)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateIndices(uint destOffset, byte[] data, uint length)
        {
            Marshal.Copy(data, 0, _indicePtr + (int)destOffset, (int)length);
        }

        public void UpdateData(uint indexBuffer, uint destOffset, byte[] data, uint length)
        {
            //Marshal.Copy(data, 0, _bufferPtrs[indexBuffer] + (int)destOffset, (int)length);
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            Gl.glNamedBufferSubData(_buffers[0].GetHandle(), 0, (uint)data.Length, handle.AddrOfPinnedObject());
            handle.Free();
        }

        public void Draw(GPUDrawCommand command)
        {
            Matrix m = Matrix.PerspectiveFovLH(70, 2.6f, 0.1f, 10);
            //TODO: this is debug impl , check if already binded
            //Gl.glBindFramebuffer(Gl.GL_FRAMEBUFFER, _framebuffer.GetHandle());
            Gl.glUseProgram(_program.GetHandle());
            Gl.glBindVertexArray(_vao.GetHandle());
            Gl.glDrawArrays(command.PirmitiveType, (int)command.FirstVertex, (int)command.VertexCount);
        }

        public void SetFramebuffer(GPUFramebuffer framebuffer)
        {
            _framebuffer = (GLFramebuffer)framebuffer;
        }
    }
}