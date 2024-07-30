import { MyEnvironment } from './my-environment';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'LibraRestaurant',
  },
  oAuthConfig: {
    issuer: 'http://localhost:8080/realms/master',
    redirectUri: baseUrl,
    clientId: 'Web',
    responseType: 'code',
    scope: 'offline_access openid profile email phone roles AdministrationService IdentityService BasketService CatalogService OrderingService PaymentService CmskitService', 
    //requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:7285',
      rootNamespace: 'LibraRestaurant',
    },
    Catalog: {
      url: 'https://localhost:7006',
      rootNamespace: 'LibraRestaurant.CatalogService',
    },
    Ordering: {
      url: "https://localhost:7003",
      rootNamespace: 'LibraRestaurant.OrderingService',
    }
  },
  mediaServerUrl:'https://localhost:44373'
} as MyEnvironment;


