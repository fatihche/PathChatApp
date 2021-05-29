"use strict";

var currentRoomName =""
var listObj = this;
listObj.allChannelList = ko.observableArray([]);

listObj.activeChannelList = ko.observableArray([]); 
listObj.messageList = ko.observableArray([]); 
$('#exampleModal').modal('show');

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/messages")
    .build();

connection.on("ReceiveMessage", function (message,group,nickName) {
    
    var index = listObj.activeChannelList().findIndex(function (d) {
        return d.name == group;
    })
    listObj.activeChannelList()[index].userMessages.push({ name: nickName, message: message, isRead: false })
 $('.active').click()
    
})

connection.on("UserListCount", function (count, group) {
    
    var index = listObj.activeChannelList().findIndex(function (d) {
        return d.name == group;
    })
    
    listObj.activeChannelList()[index]["userListCount"] = ko.computed(function () {
        return count;
    });
   
    

})

connection.on("UserConnected", function (connectionId) {
    var groupElement = document.getElementById("group");
    var option = document.createElement("option");
    option.text = connectionId;
    option.value = connectionId;
    groupElement.add(option);
})

connection.on("UserDisconnected", function (connectionId) {
    var groupElement = document.getElementById("group");
    for (var i = 0; i < groupElement.length; i++) {
        if (groupElement.options[i].value == connectionId) {
            groupElement.remove(i);
        }
    }
})

connection.start().catch(function (err) {
    return console.error(err.toString());
})




$('#joinChat').click(function () {
    
    ko.applyBindings(new ViewModel());
   
  
    $('#exampleModal').modal('hide');
    $('#chatApp').show()

})

function ViewModel() {
    var self = this; 
    
    var ID = 1
    $.ajax({
        url: '/Home/GetRooms/' + ID,
        type: 'POST',
        dataType: 'json',
        success: function (data) {
            setTimeout(function () {
                listObj.activeChannelList.removeAll();
                listObj.allChannelList.removeAll();
                data.forEach(function (entry, index) {
                    entry.userListCount = ko.observable(0)
                    entry.userMessages = ko.observableArray([])
                    if (entry.isActive) {
                        
                        connection.invoke("JoinGroup", entry.name).catch(function (err) {
                            return console.error(err.toString());
                        });
                        listObj.activeChannelList.push(entry);
                    } else {
                        listObj.allChannelList.push(entry);
                    }
                    
                });

            }, 200); 
            
        }
    });
 
}

$('#sendGroupBtn').click(function () {
    
    var message = document.getElementById("txtMessage").value;
    if (currentRoomName != "") {
        connection.invoke("SendMessageToGroup", currentRoomName, $('#recipient-name').val(), message).catch(function (err) {
            return console.error(err.toString());
        });
    }
    $('#txtMessage').val('')
    
})

$(document).on('click', '.chat-user', function (e) {
    $('#chatSection').show();
    $('#sendMessageSection').show();
    e.preventDefault()

    var that = $(this);
    var roomId = that.find('a')[0].innerText;
    currentRoomName = that.find('a')[1].innerText;
    var index = listObj.activeChannelList().findIndex(function (d) {
        return d.id == roomId;
    })
    listObj.messageList.removeAll();
    var talkHistory = listObj.activeChannelList()[index].userMessages();
    if (talkHistory && talkHistory.length > 0) {
       
        listObj.activeChannelList()[index].userMessages().forEach(data => {
            
            //data.isRead = true;
            listObj.messageList.push(data);
        })

        var a = listObj.messageList()

    }

  
   
    that.parent().find('.chat-user').removeClass('active');
    that.addClass('active');
    
   
    
});

$(document).on('click', '.chat-user-d', function (e) {
    var that = $(this);
    var roomId = that.find('a')[0].innerText;
    var index = listObj.allChannelList().findIndex(function (d) {
        return d.id == roomId;
    })
    var tmpData = listObj.allChannelList()[index]
    listObj.activeChannelList.unshift(tmpData)
    listObj.allChannelList.splice(index,index+1)
    $('.chat-user')[0].click()
    connection.invoke("JoinGroup", tmpData.name).catch(function (err) {
        return console.error(err.toString());
    });
    
});