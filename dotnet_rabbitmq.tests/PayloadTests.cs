using FluentAssertions;

namespace dotnet_rabbitmq.tests;

public class PayloadTests
{
    [Test]
    public void GivenSerializedPayload_WhenConstructing_ThenPayloadConstructedFromSerializedData()
    {
        //Given
        var timeStamp = DateTime.Now;
        var count = 42;
        var payload = new Payload(timeStamp, count);
        var json = payload.ToJson();

        //When
        var deserializedPayload = new Payload(json);

        //Then
        deserializedPayload.TimeStamp.Should().Be(timeStamp);
        deserializedPayload.Count.Should().Be(count);
        deserializedPayload.Should().BeEquivalentTo(payload);
    }
}