import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroupDirective, FormGroup, FormControl, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { AddQuestion } from 'src/app/models/add-question';
import { QuestionContent } from 'src/app/models/question-content';
import { ParaghraphService } from 'src/app/services/paraghraph.service';
import { PorsemanService } from 'src/app/services/porseman.service';
import { QuestionService } from 'src/app/services/question.service';

@Component({
  selector: 'app-porseman',
  templateUrl: './porseman.component.html',
  styleUrls: ['./porseman.component.scss']
})
export class PorsemanComponent implements OnInit {

  public state: string = 'loading';

  public paragraph: QuestionContent = {
    context: '',
    paragraphId: '',
    title: ''
  };

  @ViewChild(FormGroupDirective)
  formGroupDirective!: FormGroupDirective;

  public addQuestion: AddQuestion = new AddQuestion();
  public offset: number = 0;
  public readonly answerSubject: Subject<any>;
  public form: FormGroup = new FormGroup({
    question: new FormControl(null, [Validators.required]),
    answer: new FormControl(null)
  });

  constructor(
    private readonly service: ParaghraphService,
    private readonly porsemanService: PorsemanService,
    private readonly questionService: QuestionService) {
    this.answerSubject = new Subject();
  }

  public async ngAfterViewInit(): Promise<void> {
    await this.nextParagraph();
  }

  public async ngOnInit() {
  }

  public async nextParagraph() {
    this.state = 'loading';
    this.paragraph = await this.service.getNextParagraph();
    this.addQuestion = new AddQuestion();
    this.addQuestion.paragraphId = this.paragraph.paragraphId;
    this.resetVariables();
    this.state = 'ready';
  }

  private resetVariables() {
    this.offset = 0;
    this.formGroupDirective?.resetForm();
  }

  public async submit() {
    this.state = 'loading';
    await this.questionService.Add(this.addQuestion);
    await this.nextParagraph();
  }

  public async ask() {
    if (this.form.valid) {
      var result = await this.porsemanService.ask({
        question: this.form.get('question')?.value,
        text: this.paragraph.context
      })

      this.form.get('answer')?.setValue(result.answer);
    }
  }
}
