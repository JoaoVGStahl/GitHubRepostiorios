import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { RepositoriosComponent } from './pages/repositorios/repositorios.component';
import { FavoritosComponent } from './pages/favoritos/favoritos.component';
import { RepositoriosRelevantesComponent } from './pages/repositorios-relevantes/repositorios-relevantes.component';
import { RepositoriosUsuarioComponent } from './pages/repositorios-usuario/repositorios-usuario.component';
import { AppRoutes } from './domain/consts/routes.const';

export const routes: Routes = [
  {
    path: AppRoutes.home,
    loadComponent: () => HomeComponent
  },
  {
    path: AppRoutes.repositorios,
    loadComponent: () => RepositoriosComponent
  },
  {
    path: AppRoutes.repositoriosRelevantes,
    loadComponent: () => RepositoriosRelevantesComponent
  },
  {
    path: AppRoutes.repositoriosUsuario,
    loadComponent: () => RepositoriosUsuarioComponent
  },
  {
    path: AppRoutes.favoritos,
    loadComponent: () => FavoritosComponent
  }
];