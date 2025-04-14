import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';

interface ErrorDialogData {
  message: string;
  title?: string;
}

@Component({
  selector: 'app-error-dialog',
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatIconModule],
  template: `
    <div class="error-dialog">
      <div class="error-icon">
        <mat-icon>error_outline</mat-icon>
      </div>
      <h2 class="error-title">{{ data.title || 'Ops! Algo deu errado' }}</h2>
      <p class="error-message">{{ data.message }}</p>
      <div class="error-actions">
        <button mat-raised-button color="primary" (click)="onClose()">
          Entendi
        </button>
      </div>
    </div>
  `,
  styles: [`
    .error-dialog {
      padding: 24px;
      text-align: center;
      max-width: 400px;
    }

    .error-icon {
      color: #f44336;
      font-size: 48px;
      margin-bottom: 16px;
    }

    .error-icon mat-icon {
      font-size: 48px;
      width: 48px;
      height: 48px;
    }

    .error-title {
      color: #333;
      margin: 0 0 16px;
      font-size: 24px;
      font-weight: 500;
    }

    .error-message {
      color: #666;
      margin: 0 0 24px;
      font-size: 16px;
      line-height: 1.5;
    }

    .error-actions {
      display: flex;
      justify-content: center;
      gap: 8px;
    }
  `]
})
export class ErrorDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<ErrorDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ErrorDialogData
  ) { }

  onClose(): void {
    this.dialogRef.close();
  }
} 