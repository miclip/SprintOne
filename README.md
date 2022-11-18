Install dependencies on EC2 instance: 
~~~shell
sudo yum update
yum install httpd git
sudo yum install httpd git
sudo yum install libunwind libicu
sudo yum install dotnet-runtime-6.0 -y
~~~
Build and publish the dotnet core app as a self contained binary for linux: 
~~~shell
sudo dotnet publish sprintone.sln -c release -r linux-x64 --self-contained=true -p:publishsinglefile=true 
  -p:generateruntimeconfigurationfiles=true -o /etc/sprintone
~~~

Create a systemctl configuration file `sudo vim /etc/systemd/system/sprintone.service`
~~~ini
[Unit]
Description=Sprint One

[Service]
Type=notify
iWorkingDirectory=/etc/sprintone
ExecStart=/etc/sprintone/HelloWorld --urls=http://127.0.0.1:5000/

[Install]
WantedBy=multi-user.target

~~~

Reload the systemctl daemon, start the apache and dotnet services. Finally enable them 
so systemctl will manage these services (start after reboot).

~~~shell 
sudo systemctl daemon-reload
sudo systemctl start httpd sprintone
sudo systemctl enable httpd sprintone 
~~~