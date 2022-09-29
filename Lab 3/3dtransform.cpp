#include <GL/glut.h>

GLfloat xRotated, yRotated, zRotated;
void display()
{
  glClear(GL_COLOR_BUFFER_BIT);
  glLoadIdentity();
  glTranslatef(0.0,0.0,-4.0);
  glRotatef(xRotated,1.0,0.0,0.0);
  glRotatef(yRotated,0.0,1.0,0.0);
  glRotatef(zRotated,0.0,0.0,1.0);
  glScalef(2.0, 1.0, 1.0);
  glutWireCube(1.0);
  glFlush(); //Finish rendering
  glutSwapBuffers();
}

void idle()
{
  xRotated += 0.01;
  yRotated += 0.01;
  zRotated += -0.04;
  display();
}

void reshape(int x, int y)
{
  if (y == 0 || x == 0) return; //Nothing is visible then, so return
  //Set a new projection matrix
  glMatrixMode(GL_PROJECTION);
  glLoadIdentity();
  //Angle of view:40 degrees
  //Near clipping plane distance: 0.5
  //Far clipping plane distance: 20.0
  gluPerspective(40.0, (GLdouble) x / (GLdouble) y, 0.5, 20.0);
  glMatrixMode(GL_MODELVIEW);
  glViewport(0, 0, x, y);
}

int main(int argc, char **argv)
{
  //Initialize GLUT
  glutInit(&argc, argv);
  glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGB); //For animations you should use double buffering
  glutInitWindowSize(300, 300);
  
  //Create a window with rendering context and everything else we need
  glutCreateWindow("Cube Example");
  glPolygonMode(GL_FRONT_AND_BACK,GL_LINE);
  xRotated = yRotated = zRotated = 0.0;
  glClearColor(0.0,0.0,0.0,0.0);
  
  //Assign the two used Msg-routines
  glutDisplayFunc(display);
  glutReshapeFunc(reshape);
  glutIdleFunc(idle);

  //Let GLUT get the msgs
  glutMainLoop();
  return 0;
}