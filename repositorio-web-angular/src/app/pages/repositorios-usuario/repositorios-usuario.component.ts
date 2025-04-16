import { Component, OnInit } from '@angular/core';
import { RepositorioDTO } from '../../domain/models/repositorio.model';
import { RepositoriosApiProvider } from '../../core/providers/repositorio.api.provider';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RepositorioItemComponent } from '../../shared/components/repositorio-item/repositorio-item.component';
import { Router } from '@angular/router';
import { AppRoutes } from '../../domain/consts/routes.const';

@Component({
  selector: 'app-repositorios-usuario',
  imports: [CommonModule, FormsModule, RepositorioItemComponent],
  templateUrl: './repositorios-usuario.component.html',
  styleUrl: './repositorios-usuario.component.css'
})
export class RepositoriosUsuarioComponent implements OnInit {
  nomeUsuario: string = '';
  repositoriosRelevantes: RepositorioDTO[] = [];
  buscando = false;

  constructor(
    private repositorioService: RepositoriosApiProvider,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.nomeUsuario = 'JoaoVGStahl';
    this.buscar();
  }

  buscar(): void {
    if (this.nomeUsuario.trim() === '') {
      this.repositoriosRelevantes = [];
      return;
    }

    this.buscando = true;
    this.repositorioService.listarPorUsuario(this.nomeUsuario)
      .subscribe(repositorios => {
        this.repositoriosRelevantes = repositorios;
        this.buscando = false;
      });
  }

  voltarParaHome(): void {
    this.router.navigate([AppRoutes.home]);
  }
} 
