using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPrueba.Services
{
    public interface IService
    {
        void RealizarTarea();
        Guid ObtenerTransient();
        Guid ObtenerScoped();
        Guid ObtenerSingleton();
    }

    public class ServicioA : IService {
        private readonly ILogger<ServicioA> logger;
        private readonly ServiceTransient serviceTransient;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;

        public ServicioA(ILogger<ServicioA> logger, ServiceTransient serviceTransient, ServiceScoped serviceScoped, ServiceSingleton serviceSingleton) 
        {
            this.logger = logger;
            this.serviceTransient = serviceTransient;
            this.serviceScoped = serviceScoped;
            this.serviceSingleton = serviceSingleton;
        }
        public Guid ObtenerTransient() { return this.serviceTransient.Guid; }
        public Guid ObtenerScoped() { return this.serviceScoped.Guid; }
        public Guid ObtenerSingleton() { return this.serviceSingleton.Guid; }
        public void RealizarTarea() {
            Console.WriteLine("Realizando tarea A");
        }
    }

    public class ServicioB : IService {
        public Guid ObtenerScoped()
        {
            throw new NotImplementedException();
        }

        public Guid ObtenerSingleton()
        {
            throw new NotImplementedException();
        }

        public Guid ObtenerTransient()
        {
            throw new NotImplementedException();
        }

        public void RealizarTarea() {
            Console.WriteLine("Realizando tarea B");
        }
    }

    public class ServiceTransient {
        public Guid Guid = Guid.NewGuid();
    }

    public class ServiceScoped {
        public Guid Guid = Guid.NewGuid();
    }

    public class ServiceSingleton {
        public Guid Guid = Guid.NewGuid();
    }
}