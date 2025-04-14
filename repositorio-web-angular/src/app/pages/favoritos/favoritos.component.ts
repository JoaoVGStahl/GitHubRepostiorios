import { Component, OnInit } from '@angular/core';
import { RepositorioItemComponent } from '../../shared/components/repositorio-item/repositorio-item.component';
import { RepositorioDTO } from '../../domain/models/repositorio.model';
import { FavoritosService } from '../../application/services/favoritos.services';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-favoritos',
  imports: [CommonModule, FormsModule, RepositorioItemComponent],
  templateUrl: './favoritos.component.html',
  styleUrl: './favoritos.component.css'
})
export class FavoritosComponent implements OnInit {
  favoritos: RepositorioDTO[] = [];

  constructor(private favoritosService: FavoritosService) { }

  ngOnInit(): void {
    this.listarFavoritos();
  }

  listarFavoritos() {
    this.favoritosService.listar()
      .subscribe(favoritos => {
        this.favoritos = favoritos;
      });
  }

  onRemovidoFavoritos(repositorio: RepositorioDTO) {
    this.favoritos = this.favoritos.filter(repo => repo.id !== repositorio.id);
  }
}