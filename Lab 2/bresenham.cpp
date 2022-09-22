#define GLUT_DISABLE_ATEXIT_HACK
#include "stdio.h"
#include "math.h"
#include "GL/glut.h"

int x1;
int y_1;
int x2;
int y_2;

void SetPixel(int x, int y)
{
  //glColor3f(1.0,0.0,0.0);
  glBegin(GL_POINTS);
    glVertex2i(x,y);
  glEnd();
}

void Bresenham_Line(void)
{
  int dx = 0;
  int dy = 0;
  int x = 0;
  int y = 0;
  int inc = 0;
  int temp = 0;
  int i = 0;
  int e = 0;

  glClear(GL_COLOR_BUFFER_BIT);
  glColor3f(1.0,0.0,0.0);
  glPointSize(2.0);

  dx = x2 - x1;
  dy = y_2 - y_1;

  if(dx * dy >= 0)
    inc = 1;
  else
    inc = -1;

  if(abs(dx) >= abs(dy) )
  {
    if(dy < 0)
    {
      temp = x1;
      x1 = x2;
      x2 = temp;
      temp = y_1;
      y_1 = y_2;
      y_2 = temp;
    }
    dx = abs(dx);
    dy = abs(dy);

    e = -dx;
    x = x1;
    y = y_1;

    for(i = 0; i <= dx; i ++)
    {
      SetPixel(x, y);
      x = x + inc;
      e = e + 2*dy;
      if(e >= 0)
      {
        y = y + 1;
        e = e - 2 * dx;
      }
    }
  }
  else
  {
    if(dx < 0)
    {
      temp = x1;
      x1 = x2;
      x2 = temp;
      temp = y_1;
      y_1 = y_2;
      y_2 = temp;
    }
    dx = abs(dx);
    dy = abs(dy);

    e = -dy;
    x = x1;
    y = y_1;

    for(i = 0; i <= dy; i ++)
    {
      SetPixel(x, y);
      y = y + inc;
      e = e + 2 * dx;
      if(e >= 0)
      {
        x = x + 1;
        e = e - 2 * dy;
      }
    }
  }
  glFlush();
}

void ChangeSize(GLsizei w, GLsizei h)
{
  GLfloat nRange = 150.0f;
  if(h == 0) h = 1;
  glViewport(0, 0, w, h);
  glMatrixMode(GL_PROJECTION);
  glLoadIdentity();
  gluOrtho2D(-nRange, nRange, -nRange, nRange);
  glMatrixMode(GL_MODELVIEW);
  glLoadIdentity();
}

int main(int argc, char * argv[])
{
  glutInit(&argc,argv);

  printf("Please input:x1,y_1,x2,y_2:\n");
  scanf("%d,%d,%d,%d",&x1,&y_1,&x2,&y_2);

  glutInitDisplayMode(GLUT_SINGLE|GLUT_RGB);
  glutInitWindowSize(400,400);
  glutCreateWindow("Breseham Line");
  glClearColor(1.0,1.0,1.0,0.0);

  glutReshapeFunc(ChangeSize);
  glutDisplayFunc(Bresenham_Line);

  glutMainLoop();
  return 0;
}
