import { Component, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { Workshop } from 'src/app/models/workshop.model';

@Component({
  selector: 'app-workshop-detail',
  templateUrl: './workshop-detail.component.html',
  styleUrls: ['./workshop-detail.component.scss']
})
export class WorkshopDetailComponent implements OnChanges {
  @Input() workshop!: Workshop;
  @Output() fechar = new EventEmitter<void>();

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['workshop'] && this.workshop && typeof this.workshop.dataRealizacao === 'string') {
      this.workshop.dataRealizacao = new Date(this.workshop.dataRealizacao);
    }
  }

  fecharDetalhe() {
    this.fechar.emit();
  }
}
