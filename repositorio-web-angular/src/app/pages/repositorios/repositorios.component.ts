import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AppRoutes } from '../../domain/consts/routes.const';
import { RepositoriosApiProvider } from '../../core/providers/repositorio.api.provider';
import { RepositorioDTO } from '../../domain/models/repositorio.model';
import { RepositorioItemComponent } from '../../shared/components/repositorio-item/repositorio-item.component';

@Component({
  selector: 'app-repositorios',
  standalone: true,
  imports: [CommonModule, FormsModule, RepositorioItemComponent],
  templateUrl: './repositorios.component.html',
  styleUrls: ['./repositorios.component.css']
})
export class RepositoriosComponent {
  nomeRepositorio: string = '';
  repositorios: RepositorioDTO[] = [];
  buscando = false;

  constructor(
    private repositorioService: RepositoriosApiProvider,
    private router: Router
  ) { }

  buscar(): void {
    if (this.buscando) return;

    if (this.nomeRepositorio.trim() === '') {
      this.repositorios = [];
      return;
    }

    this.buscando = true;
    this.repositorioService.listar(this.nomeRepositorio)
      .subscribe(repositorios => {
        this.repositorios = repositorios;
        this.buscando = false;
      });
  }

  voltarParaHome(): void {
    this.router.navigate([AppRoutes.home]);
  }
} 