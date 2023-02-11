set WORKSPACE=..
set GEN_CLIENT=%WORKSPACE%\Luban\Luban.ClientServer\Luban.ClientServer.exe 
set CONF_ROOT=%WORKSPACE%\Assets\GameMain\DataTables

%GEN_CLIENT% -j cfg --^
 -d %CONF_ROOT%\Defines\__root__.xml ^
 --input_data_dir %CONF_ROOT%\Data ^
--output_data_dir %CONF_ROOT%\Json ^
 --gen_types data_json ^
 -s all
pause