import { AfterViewInit, Component, DoCheck, ElementRef, OnChanges, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { FormControl, FormGroup, FormGroupDirective, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { AddQuestion } from 'src/app/models/add-question';
import { QuestionContent } from 'src/app/models/question-content';
import { QuestionStruct } from 'src/app/models/question-struct';
import { ParaghraphService } from 'src/app/services/paraghraph.service';
import { QuestionService } from 'src/app/services/question.service';


@Component({
  selector: 'app-add-question',
  templateUrl: './add-question.component.html',
  styleUrls: ['./add-question.component.scss']
})
export class AddQuestionComponent implements OnInit, AfterViewInit {

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
    answer: new FormControl(null, [Validators.required])
  });

  constructor(
    private readonly service: ParaghraphService,
    private readonly questionService: QuestionService) {
    this.answerSubject = new Subject();
  }

  public async ngAfterViewInit(): Promise<void> {
    document.onselectionchange = this.select;
    await this.nextParagraph();
  }

  public async ngOnInit() {
    this.answerSubject.subscribe((selection) => this.setVariables(selection));
    this.setSubject();
  }

  public async nextParagraph() {
    this.state = 'loading';
    this.paragraph = await this.service.getNextParagraph();
    this.addQuestion = new AddQuestion();
    this.addQuestion.paragraphId = this.paragraph.paragraphId;
    this.resetVariables();
    this.state = 'ready';
  }

  private setSubject() {
    let documentItem: any = document;
    documentItem.answerSubject = this.answerSubject;
  }

  private setVariables(selection: unknown) {
    this.form.get('answer')?.setValue((selection as any)?.toString());
    const anchorOffest = (selection as any)?.anchorOffset;
    const focusOffset = (selection as any)?.focusOffset;
    this.offset = anchorOffest < focusOffset ? anchorOffest : focusOffset;
  }

  public select(event: Event) {
    var selection = window.getSelection() as any;
    const node = selection?.baseNode ?? selection?.focusNode;
    if (node?.parentElement?.id == 'context') {
      let documentItem: any = document;
      documentItem.answerSubject.next(selection);
    }
  }

  public addNewQuestion() {
    if (this.form.valid) {
      this.addQuestion.questions.push({
        question: this.form.get('question')?.value,
        answers: [{ answerStart: this.offset, text: this.form.get('answer')?.value }]
      });

      this.resetVariables();
    }
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

  public remove(index: number) {
    this.addQuestion.questions.splice(index, 1);
  }
}
