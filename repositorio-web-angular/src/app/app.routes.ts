import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { RepositoriosComponent } from './pages/repositorios/repos.component';
import { FavoritosComponent } from './pages/favoritos/favoritos.component';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => HomeComponent
  },
  {
    path: 'repositorios',
    loadComponent: () => RepositoriosComponent
  },
  {
    path: 'favoritos',
    loadComponent: () => FavoritosComponent
  }
];