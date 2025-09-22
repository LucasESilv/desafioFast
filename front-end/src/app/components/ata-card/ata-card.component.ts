import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Ata } from 'src/app/models/ata.model';
import { Colaborador } from 'src/app/models/colaborador.model';

@Component({
  selector: 'app-ata-card',
  templateUrl: './ata-card.component.html',
  styleUrls: ['./ata-card.component.scss']
})
export class AtaCardComponent {
  @Input() ata!: Ata;
  @Output() verWorkshop = new EventEmitter<void>();

  detalharWorkshop() {
    this.verWorkshop.emit();
  }

  isLastColab(colab: Colaborador): boolean {
    return this.ata.colaboradores.indexOf(colab) === this.ata.colaboradores.length - 1;
  }
}
