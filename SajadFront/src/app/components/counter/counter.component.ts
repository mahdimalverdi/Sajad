import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-counter',
  templateUrl: './counter.component.html',
  styleUrls: ['./counter.component.scss']
})
export class CounterComponent implements OnChanges {

  public currentCount: number = 0;

  @Input()
  public count: number = 0;
  @Input()
  public title: string = '';

  @Input()
  public time: number = 3000;

  private readonly iterationCount = 100;

  constructor() { }

  private interval: any = null;

  ngOnChanges(changes: SimpleChanges): void {
    clearInterval(this.interval);

    var diff = this.count - this.currentCount;

    this.interval = setInterval(() => {
      this.currentCount += Math.round(diff / this.iterationCount);
      if (this.currentCount >= this.count) {
        this.currentCount = this.count;
        clearInterval(this.interval);
      }
    }, this.time / this.iterationCount);
  }



}
