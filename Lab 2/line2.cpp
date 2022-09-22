/*
Purpose: Drawing Primitives Line in OpenGL
*/
#define GLUT_DISABLE_ATEXIT_HACK
#include <GL/glut.h>

GLvoid display()
{
  /* Do all your OpenGL rendering here */
  glClear(GL_COLOR_BUFFER_BIT);
  glColor3f(0.0,0.1,0.0);
  glPointSize(8.0);
  glLineWidth(5.0);
  glBegin(GL_LINE_STRIP);
    glVertex2f(-0.75,-0.75);
    glVertex2f(-0.25,-0.25);
    glVertex2f(0.0,0.5);
    glVertex2f(0.75,0.5);
  glEnd();
  glFlush();
}

void init(void)
{
  /* set background color */
  glClearColor(1.0,1.0,0.0,1.0);
}

int main(int argc, char* argv[])
{
  GLint width;
  GLint height;

  glutInit(&argc, argv);
  glutInitDisplayMode(GLUT_SINGLE | GLUT_RGB);

  width = glutGet(GLUT_SCREEN_WIDTH);
  height = glutGet(GLUT_SCREEN_HEIGHT);
  glutInitWindowPosition(width/4, height/4);
  glutInitWindowSize(width/2, height/2);
  glutCreateWindow(argv[0]);

  glutDisplayFunc( display );
  init();

  glutMainLoop();

  return 0;
}
