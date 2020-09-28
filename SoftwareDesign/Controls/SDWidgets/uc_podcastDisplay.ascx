<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_podcastDisplay.ascx.cs" Inherits="SoftwareDesign.Controls.SDWidgets.uc_podcastDisplay" %>

<asp:Label ID="txtPodcastURL" runat="server" />

<table>
    <asp:Repeater runat="server" ID="podcastRepeater">
        <ItemTemplate>
            <tr>
		        <td>
                    <h2><a href="<%# Eval("Link") %>"><%# Eval("Title") %></a></h2>
                    <strong style="font-size: 80%;"><%# Eval("PubDate") %></strong>
                    <p><%# Eval("Description") %></p>
                    <div class="audio_wrapper">
                        <audio controls="controls" preload="none" onpause="pauseCheck(this)" onplay="clickCheck(this)" onended="endCheck(this)">
                            <source src="<%# Eval("Link") %>">
                            <p>Your browser does not support the audio element, but you can <a href="<%# Eval("Link") %>">download this mp3</a> file.</p>
                        </audio>
                        <button class="rewindBtn" onclick="rewindTime(this)" type="button"><i class="fa fa-backward" aria-hidden="true"></i></button>
                        <button class="forwardBtn" onclick="forwardTime(this)" type="button"><i class="fa fa-forward" aria-hidden="true"></i></button>
                    </div>
                    <%--<object style="float: left;" type="application/x-shockwave-flash" data="https://flash-mp3-player.net/medias/player_mp3_mini.swf" width="200" height="20">
                        <param name="movie" value="https://flash-mp3-player.net/medias/player_mp3_mini.swf">
                        <param name="bgcolor" value="#eeeeee">
                        <param name="FlashVars" value="mp3=<%# Eval("Link") %>&amp;bgcolor=eeeeee&amp;buttoncolor=000000&amp;slidercolor=555555">
                    </object>--%>
                </td>
	        </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
<script>
    $(document).ready(function () {
        var x = 0;
        var y = 0;
        var z = 0;
        $('audio').each(function () {
            $(this).attr('id', 'audio_' + x);
            x += 1;
        });
        $('button.rewindBtn').each(function () {
            $(this).attr('id', 'rewindbtn_' + y);
            y += 1;
        });
        $('button.forwardBtn').each(function () {
            $(this).attr('id', 'forwardbtn_' + z);
            z += 1;
        });
        var elemsRew = document.getElementsByClassName("rewindBtn");
        var total1 = elemsRew.length
        for (var i = 0; i < total1; i++) {
            elemsRew[i].disabled = true;
        }
        var elemsForw = document.getElementsByClassName("forwardBtn");
        var total2 = elemsForw.length
        for (var i = 0; i < total2; i++) {
            elemsForw[i].disabled = true;
        }
    });
    function clickCheck(audioClicked) {
        var audioid = audioClicked.getAttribute('id') //Get audio clicked id
        var a = audioid.split("_"); //Split audio id to get number at the end (var c)
        var b = a[0];
        var c = a[1];
        var rew = document.getElementById("rewindbtn_" + c);
        var forw = document.getElementById("forwardbtn_" + c);
        rew.disabled = false;
        forw.disabled = false;
        
    }
    function pauseCheck(audioPaused) {
        var audioid = audioPaused.getAttribute('id') //Get audio clicked id
        var a = audioid.split("_"); //Split audio id to get number at the end (var c)
        var b = a[0];
        var c = a[1];
        var rew = document.getElementById("rewindbtn_" + c);
        var forw = document.getElementById("forwardbtn_" + c);
        rew.disabled = true;
        forw.disabled = true;
    }
    function endCheck(audioStopped) {
        var audioid = audioStopped.getAttribute('id') //Get audio clicked id
        var a = audioid.split("_"); //Split audio id to get number at the end (var c)
        var b = a[0];
        var c = a[1];
        var rew = document.getElementById("rewindbtn_" + c);
        var forw = document.getElementById("forwardbtn_" + c);
        rew.disabled = true;
        forw.disabled = true;
    }

    function rewindTime(rewind) {
        var btnid = rewind.getAttribute('id') //Get button clicked id
        var a = btnid.split("_"); //Split button id to get number at the end (var c)
        var b = a[0];
        var c = a[1];
        var aud = document.getElementById("audio_" + c);
        aud.currentTime -= 15;
    }
    function forwardTime(forward) {
        var btnid = forward.getAttribute('id') //Get button clicked id
        var a = btnid.split("_"); //Split button id to get number at the end (var c)
        var b = a[0];
        var c = a[1];
        var aud = document.getElementById("audio_" + c);
        aud.currentTime += 15;
    }
</script>