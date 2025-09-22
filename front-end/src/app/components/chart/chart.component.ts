import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { Ata } from 'src/app/models/ata.model';
import { ChartData, ChartType } from 'chart.js';

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.scss']
})
export class ChartComponent implements OnChanges {
  @Input() atas: Ata[] = [];

  barChartData: ChartData<'bar'> = {
    labels: [],
    datasets: [
      { data: [], label: 'Workshops Participados' }
    ]
  };

  pieChartData: ChartData<'pie'> = {
    labels: [],
    datasets: [
      { data: [] }
    ]
  };

  barChartType: ChartType = 'bar';
  pieChartType: ChartType = 'pie';

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['atas']) {
      this.gerarGraficos();
    }
  }

  gerarGraficos() {
    const countColab: { [nome: string]: number } = {};
    this.atas.forEach(ata =>
      ata.colaboradores.forEach(colab => {
        countColab[colab.nome] = (countColab[colab.nome] || 0) + 1;
      })
    );
    this.barChartData.labels = Object.keys(countColab);
    this.barChartData.datasets[0].data = Object.values(countColab);

    this.pieChartData.labels = this.atas.map(ata => ata.workshop.nome);
    this.pieChartData.datasets[0].data = this.atas.map(ata => ata.colaboradores.length);
  }
}
